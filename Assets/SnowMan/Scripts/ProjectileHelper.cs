﻿using UnityEngine;
using System.Collections;

public class ProjectileHelper : MonoBehaviour
{

    public float damage = 35f;

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision occured between : " + collision.gameObject);
        //if (collision.gameObject.tag == "Enemy" )
        //{
        //    EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        //    enemy.ApplyDamage(35f);
        //}
        //else if (collision.gameObject.tag == "Player")
        //{
        //    CharacterControllerVR enemy = collision.gameObject.GetComponent<CharacterControllerVR>();
        //    enemy.ApplyDamage(35f);
        //}

        if (collision.gameObject.tag == "Terrain")
        {
            // todo : despawn after x seconds
            GameObject.Destroy(gameObject);

        }
    }
}
