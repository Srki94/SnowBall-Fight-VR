using UnityEngine;
using System.Collections;

public class ProjectileHelper : MonoBehaviour {

    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" )
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.ApplyDamage(35f);
        }
        else if (collision.gameObject.tag == "Player")
        {
            CharacterControllerVR enemy = collision.gameObject.GetComponent<CharacterControllerVR>();
            enemy.ApplyDamage(35f);
        }
        GameObject.Destroy(gameObject);
    }
}
