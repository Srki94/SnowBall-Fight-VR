using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour {

    public GameObject Player;
    public float enemyShootTimer;
    public float enemySpawnTimer;
    EnemyManager enemySpawner;

    public int enemiesToNextLevel = 0;

    void Start()
    {
        if (!enemySpawner) { enemySpawner = GetComponent<EnemyManager>(); }
        if (!Player) { Player = GameObject.FindWithTag("Player"); }
    }


    private void Update()
    {

    }

    
}
