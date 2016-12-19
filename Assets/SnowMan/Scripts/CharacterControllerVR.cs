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
        //Physics.IgnoreCollision(GetComponent<BoxCollider>(), snowBall.GetComponent<Collider>());
        snowBall.AddComponent<Rigidbody>().AddForce(snowBallSpawnPoint.forward * 100f);
    }

    public void ApplyDamage(float dmg)
    {
        HP -= dmg;
        if (HP <= 0f)
        {
            /// todo : gameover s
        }
    }
}
