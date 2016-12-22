﻿using Assets.SnowMan.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] enemySpawnPositions;
    public GameObject[] enemyPrefabs;
    public Transform playerPos;
    public float SpawnTimer = 5f;
    public int maxEnemiesInLevel = 5;

    List<GameObject> enemiesInScene = new List<GameObject>();
    List<SpawnPosition> enemiesSpawnPositions = new List<SpawnPosition>();
    GameMgr gmr;

    void Start()
    {
        gmr = GetComponent<GameMgr>();
        SpawnEnemy();
    }

    void Update()
    {
        SpawnTimer -= 1f * Time.deltaTime;
        if (SpawnTimer <= 0f 
            && !gmr.IsGameOver)
        {
            SpawnTimer = 3f;
            SpawnEnemy();
        }
    }

    public void RemoveAllEnemies()
    {
        for (var i = 0; i <= enemiesInScene.Count - 1; i++)
        {
            GameObject.Destroy(enemiesInScene[i]);
        }
    }

    public void DespawnAllEnemies()
    {
        foreach(GameObject enemy in enemiesInScene)
        {
            enemy.GetComponent<EnemyController>().Die();
        }

        enemiesInScene.Clear();
    }

    public void SpawnEnemy()
    { 
        if (enemiesInScene.Count >= maxEnemiesInLevel
            || gmr.EnemiesToNextLevel <= 0)
        {
            return;
        }
        var f = Random.Range(0, enemySpawnPositions.Length);

        GameObject dragon = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)],
                                                    enemySpawnPositions[f].position, enemySpawnPositions[f].rotation) as GameObject;
        dragon.GetComponent<EnemyController>().player = playerPos;
        dragon.GetComponent<EnemyController>().enemyMgr = this;
        enemiesInScene.Add(dragon);
    }

    public void NotifyEnemyDeath(GameObject unit)
    {
        gmr.EnemiesToNextLevel--;
        enemiesInScene.Remove(unit);

        gmr.AddScore(1);
    }

}
