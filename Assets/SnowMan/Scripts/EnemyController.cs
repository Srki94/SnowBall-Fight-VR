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

    NavMeshAgent thisAgent;
    Animator animator;
    float shootDelay = 1.5f;
    bool isDying = false;

    void Start()
    {
        thisAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        HP = 100f;
    }

    void Update()
    {
        if (isDying) { return; }

        if (Vector3.Distance(transform.position, player.position) > 5.5f)
        {
            thisAgent.SetDestination(player.position);
            animator.SetTrigger("Walk");
        }
        else
        {
            animator.SetTrigger("Idle 2");
            thisAgent.speed = 0f;

            if (shootDelay <= 0f)
            {
                transform.LookAt(player);
                Shoot(player.position);
                shootDelay = 1.5f;
            }
            else
            {
                shootDelay -= 1f * Time.deltaTime;
            }

        }


    }

    void Shoot(Vector3 target)
    {
        animator.SetTrigger("Basic Attack");
        GameObject bullet = Instantiate(ballPrefab, ballSpawnPos.position, ballSpawnPos.rotation) as GameObject;
        bullet.AddComponent<Rigidbody>().AddForce(Vector3.forward * 1500f);
    }

    public void ApplyDamage(float dmg)
    {
        HP -= dmg;
        if (HP <= 0f)
        {
            isDying = true;
            animator.SetTrigger("Die");
            GameObject.DestroyObject(gameObject, 2f);
        }
    }

}
