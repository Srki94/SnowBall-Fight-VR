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
        if (Input.GetMouseButtonDown(0) && gm.controllerType == GameMgr.ControllerType.Touch)
        {
            ThrowSnowball();
        }
    }

    void ThrowSnowball()
    {
        GameObject snowBall = Instantiate(snowBallPrefab, snowBallSpawnPoint.position, snowBallSpawnPoint.rotation) as GameObject;
        snowBall.tag = "PlayerAmmo";
        snowBall.AddComponent<Rigidbody>().AddForce(snowBallSpawnPoint.forward * 500f);
        GameMgr.playerScoreData.ballsThrown++;
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
