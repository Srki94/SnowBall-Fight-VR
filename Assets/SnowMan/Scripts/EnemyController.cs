using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public Transform ballSpawnPos;
    public GameObject ballPrefab;
    public bool isMovable = true;
    public float HP;
    public EnemyManager enemyMgr;
    public float moveSpeed = .6f;

    NavMeshAgent thisAgent;
    Animator animator;
    float shootRate = 1.5f;
    float sliceAmount = 0f;
    bool isDying = false;
    Renderer thisRend;

    void Start()
    {
        thisAgent = GetComponent<NavMeshAgent>();
        thisRend = GetComponentInChildren<Renderer>();
        animator = GetComponent<Animator>();
        HP = 100f;
        thisAgent.speed = moveSpeed;
    }

    void Update()
    {
        if (isDying)
        {
            sliceAmount = Mathf.Lerp(sliceAmount, 1f, 1f * Time.deltaTime);
            thisRend.material.SetFloat("_SliceAmount", sliceAmount);
        }
        else
        {
            if (Vector3.Distance(transform.position, player.position) > 5.5f)
            {
                thisAgent.SetDestination(player.position);
                thisAgent.speed = moveSpeed;
                if (!animator.GetBool("IsWalking"))
                {
                    animator.SetBool("IsWalking", true);
                }
            }
            else
            {
                if (animator.GetBool("IsWalking"))
                {
                    animator.SetBool("IsWalking", false);
                }
                thisAgent.speed = 0f;

                if (shootRate <= 0f)
                {
                    Shoot(player.position);
                    shootRate = 1.5f;
                }
                else
                {
                    shootRate -= 1f * Time.deltaTime;
                }
            }

        }
    }

    void Shoot(Vector3 target)
    {
        animator.SetTrigger("Basic Attack");
        StartCoroutine(SpawnAfterDelay(.3f));
    }

    IEnumerator SpawnAfterDelay(float t)
    {
        yield return new WaitForSeconds(t);
        GameObject bullet = Instantiate(ballPrefab, ballSpawnPos.position, ballSpawnPos.rotation) as GameObject;
        bullet.transform.LookAt(player);
        ballSpawnPos.LookAt(player);
        gameObject.transform.LookAt(player);
        bullet.tag = "CPUAmmo";
        bullet.AddComponent<Rigidbody>().AddForce(ballSpawnPos.forward * 500f);
    }

    public void ApplyDamage(float dmg)
    {
        HP -= dmg;
        if (HP <= 0f && !isDying)
        {
            isDying = true;
            animator.Play("die");

            GameObject.DestroyObject(gameObject, 2f);
            enemyMgr.NotifyEnemyDeath();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerAmmo"
            && collision.gameObject.GetComponent<ProjectileHelper>().CanDamage)
        {
            collision.gameObject.GetComponent<ProjectileHelper>().CanDamage = false;
            ApplyDamage(collision.gameObject.GetComponent<ProjectileHelper>().damage);
            GameObject.Destroy(collision.gameObject);
        }
    }

    void SwitchAnimState(int animStateID)
    {

    }
}
