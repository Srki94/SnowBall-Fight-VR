using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerVR : MonoBehaviour
{
    public Transform snowBallSpawnPoint;
    public GameObject snowBallPrefab;
    public float HP;
    public bool isDead = false;
    
    GameMgr gm;
    AudioSource sfxSource;
    float autoShootTimer = 0f;

    void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameMgr>();
        sfxSource = GetComponent<AudioSource>();

        if (!snowBallSpawnPoint) { Debug.LogError("Snowball spawn point not set!"); }
        if (!gm) { Debug.LogError("Couldn't find game manager in the scene"); }
        MagnetSensor.OnCardboardTrigger += MagnetSensor_OnCardboardTrigger;
    }

    private void MagnetSensor_OnCardboardTrigger()
    {
        if (gm.controllerType == GameMgr.ControllerType.Magnet)
        {
            ThrowSnowball();
        }
    }

    void Update()
    {
       // if (isDead)
       // {
       //    // if (!gm.IsGameOver)
       //    // {
       //    //     gm.InitGameOver();
       //    // }
       //     return;
       // }
        if (Input.GetMouseButtonDown(0) && GAMESESSION.controllerType == GameMgr.ControllerType.Touch)
        {
            ThrowSnowball();
        }
        else if (GAMESESSION.controllerType == GameMgr.ControllerType.Auto)
        { // if we're looking at the enemy cast ball every x seconds
            autoShootTimer -= 1f * Time.deltaTime;
            if (autoShootTimer <= 0f)
            {
                // RaycastHit hit;
                // if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit))
                // {
                //    
                //     Debug.Log(hit.transform);
                //
                //     Debug.DrawRay(transform.position, Camera.main.transform.forward, Color.red, 10f);
                //     if (hit.transform.tag == "Enemy")
                //     {
                //         Debug.Log("Can see dragon");
                //     }
                // }

                // RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward);
                //
                // foreach(var hit in hits)
                // {
                //     if (hit.transform.tag == "Enemy")
                //     {
                //         ThrowSnowball();
                //     }
                // }
                ThrowSnowball();
                autoShootTimer = 0.4f;
            }
        }
    }

    void ThrowSnowball()
    {
        GameObject snowBall = Instantiate(snowBallPrefab, snowBallSpawnPoint.position, snowBallSpawnPoint.rotation) as GameObject;
        snowBall.tag = "PlayerAmmo";
        snowBall.AddComponent<Rigidbody>().AddForce(snowBallSpawnPoint.forward * 500f);
        GAMESESSION.SCORE.ballsThrown++;
    }

    public void ApplyDamage(float dmg)
    {
        HP -= dmg;
        if (HP <= 0f && !isDead)
        {
            isDead = true;
            gm.InitGameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CPUAmmo"
            && collision.gameObject.GetComponent<ProjectileHelper>().CanDamage)
        {
            sfxSource.clip = gm.GetComponent<SFXBank>().GetSFX("OnPlayerHit");
            sfxSource.Play();

            collision.gameObject.GetComponent<ProjectileHelper>().CanDamage = false;
            ApplyDamage(collision.gameObject.GetComponent<ProjectileHelper>().damage);
            GameObject.Destroy(collision.gameObject);
        }
    }
}
