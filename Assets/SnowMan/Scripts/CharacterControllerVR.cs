using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CharacterControllerVR : MonoBehaviour
{
    public Transform snowBallSpawnPoint;
    public GameObject snowBallPrefab;
    public float  HP;

    void Start()
    {
        if (!snowBallSpawnPoint)
        {
            Debug.LogError("Snowball spawn point not set!");
        }
        
    }

    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse press");
            ThrowSnowball();
        }
    }

    void ThrowSnowball()
    {
        GameObject snowBall = Instantiate(snowBallPrefab, snowBallSpawnPoint.position, snowBallSpawnPoint.rotation) as GameObject;
        snowBall.tag = "PlayerAmmo";
        snowBall.AddComponent<Rigidbody>().AddForce(snowBallSpawnPoint.forward * 500f);
    }

    public void ApplyDamage(float dmg)
    {
        HP -= dmg;
        if (HP <= 0f)
        {
            /// todo : gameover s
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
