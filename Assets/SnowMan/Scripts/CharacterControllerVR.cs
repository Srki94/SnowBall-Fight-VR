using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CharacterControllerVR : MonoBehaviour
{
    public Transform snowBallSpawnPoint;
    public GameObject snowBallPrefab;
    public float  HP;
    public bool isDead = false;

    GameMgr gm;

    void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameMgr>();

        if (!snowBallSpawnPoint) { Debug.LogError("Snowball spawn point not set!");}
        if (!gm) { Debug.LogError("Couldn't find game manager in the scene"); }
    }

    void Update()
    {
        if (isDead) {
            Debug.Log("DIED");
            return; }
        if (Input.GetMouseButtonDown(0))
        {
            ThrowSnowball();
        }
    }

    void ThrowSnowball()
    {
        Debug.Log("Threw ball");
        GameObject snowBall = Instantiate(snowBallPrefab, snowBallSpawnPoint.position, snowBallSpawnPoint.rotation) as GameObject;
        snowBall.tag = "PlayerAmmo";
        snowBall.AddComponent<Rigidbody>().AddForce(snowBallSpawnPoint.forward * 500f);
    }

    public void ApplyDamage(float dmg)
    {
        HP -= dmg;
        if (HP <= 0f)
        {
            isDead = true;
            gm.InitGameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CPUAmmo")
        {
            ApplyDamage(collision.gameObject.GetComponent<ProjectileHelper>().damage);
            GameObject.Destroy(collision.gameObject);
        }
    }
}
