using Assets.SnowMan.Scripts.Data;
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
    public SnowmanAnnouncerController snowmanAnnouncer;

    List<GameObject> enemiesInScene = new List<GameObject>();
    List<SpawnPosition> enemiesSpawnPositions = new List<SpawnPosition>();
    GameMgr gmr;
    bool isCountingDown;
    float initLvlCountdown = 5f;

    void Start()
    {
        gmr = GetComponent<GameMgr>();
        snowmanAnnouncer = gmr.announcerSnowmanGO.GetComponent<SnowmanAnnouncerController>();
        InitNewLevelCountdown();
        SpawnEnemy();
    }

    public void InitNewLevelCountdown()
    {
        snowmanAnnouncer.gameObject.SetActive(true);
        snowmanAnnouncer.SetActiveElement(SnowmanAnnouncerController.AnnouncerType.NewLevel);
        isCountingDown = true;
        initLvlCountdown = 5f;
    }

    void Update()
    {
        if (isCountingDown)
        {
            initLvlCountdown -= 1f * Time.deltaTime;
            if (initLvlCountdown < 0f)
            {
                isCountingDown = false;
                snowmanAnnouncer.Despawn();
                SpawnTimer = 0.3f;
            }
            else
            {
                snowmanAnnouncer.UpdateCountdownText(Mathf.RoundToInt(initLvlCountdown).ToString());
            }
        }
        else
        {
            SpawnTimer -= 1f * Time.deltaTime;
            if (SpawnTimer <= 0f
                && !gmr.IsGameOver)
            {
                SpawnTimer = 3f;
                SpawnEnemy();
            }
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
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (var i =0; i<= enemies.Length-1; i++)
        {
            enemies[i].GetComponent<EnemyController>().Die();
        }
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
      //  enemiesInScene.Remove(unit);

        gmr.AddScore(1);
    }

}
