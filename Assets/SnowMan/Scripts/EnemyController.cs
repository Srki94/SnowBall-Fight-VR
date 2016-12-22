using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public Transform ballSpawnPos;
    public GameObject ballPrefab;
    public bool isMovable = true;
    public float HP;
    public EnemyManager enemyMgr;
    public float moveSpeed = .6f;

    SFXBank sfxStorage;
    NavMeshAgent thisAgent;
    Animator animator;
    float shootRate = 1.5f;
    float sliceAmount = 0f;
    bool isDying = false;
    Renderer thisRend;
    AudioSource sfxSource;
    
    public AudioMixerGroup snowballSFXMixer;
    public AudioMixerGroup masterSFXMixer;

    void Start()
    {
        thisAgent = GetComponent<NavMeshAgent>();
        thisRend = GetComponentInChildren<Renderer>();
        animator = GetComponent<Animator>();
        sfxSource = GetComponent<AudioSource>();
        sfxStorage = GameObject.FindWithTag("GameController").GetComponent<SFXBank>();
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
                    sfxSource.outputAudioMixerGroup = masterSFXMixer;
                    sfxSource.clip = sfxStorage.GetSFX("OnEnemyWalk");
                    sfxSource.loop = true;
                    sfxSource.Play();
                }
            }
            else
            {
                if (animator.GetBool("IsWalking"))
                {
                    animator.SetBool("IsWalking", false);
                    sfxSource.clip = null;
                }
                thisAgent.speed = 0f;

                if (shootRate <= 0f)
                {
                    shootRate = 1.5f;
                    Shoot(player.position);
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
        GameMgr.playerScoreData.monstersHit++;

        if (HP <= 0f && !isDying)
        {
           // GameMgr.playerScoreData.monstersKilled++;
            GameMgr.playerScoreData.sessionScore++;

            if (!isDying)
            {
                GetComponent<BoxCollider>().enabled = false;
                sfxSource.clip = sfxStorage.GetSFX("OnEnemyDeath");
                sfxSource.loop = false;
                sfxSource.Play();
            }
            isDying = true;
            animator.Play("die");

            
            GameObject.DestroyObject(gameObject, 2f);
            enemyMgr.NotifyEnemyDeath(gameObject);
        }
    }

    public void Die()
    {
        ApplyDamage(100f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerAmmo"
            && collision.gameObject.GetComponent<ProjectileHelper>().CanDamage)
        {
            sfxSource.outputAudioMixerGroup = snowballSFXMixer;
            sfxSource.clip = sfxStorage.GetSFX("OnEnemyHit");
            sfxSource.loop = false;
            sfxSource.Play();
            collision.gameObject.GetComponent<ProjectileHelper>().CanDamage = false;
            ApplyDamage(collision.gameObject.GetComponent<ProjectileHelper>().damage);
            GameObject.Destroy(collision.gameObject);
        }
    }

    void SwitchAnimState(int animStateID)
    {

    }
}
