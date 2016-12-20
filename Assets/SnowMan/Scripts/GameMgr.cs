using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour {

    public GameObject Player;
    public float enemyShootTimer;
    public float enemySpawnTimer;
    EnemyManager enemySpawner;
    public int currScore = 0;

    public int enemiesToNextLevel = 0;

    void Start()
    {
        if (!enemySpawner) { enemySpawner = GetComponent<EnemyManager>(); }
        if (!Player) { Player = GameObject.FindWithTag("Player"); }
        DontDestroyOnLoad(this);
    }


    private void Update()
    {

    }

    public void AddScore(int val)
    {
        currScore += val;
    }

    public void LoadNextLevel()
    {

    }

    public void InitGameOver()
    {

    }
}
