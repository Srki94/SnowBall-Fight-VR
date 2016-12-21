using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public enum DiffModifier { Easy, Medium, Hard, None };
    public enum GameplayType { VR = 0, Gyro = 1};
    public enum ControllerType { Touch = 0,Magnet = 1, Auto = 2 }

    public GameObject Player;
    public float enemyShootTimer = 0f;
    public float enemySpawnTimer = 0f;
    public int currScore = 0;
    public float snowballDamageToEnemies = 0f;
    public float snowballDamageToPlayer = 0f;
    public DiffModifier difficulty = DiffModifier.None;
    public GameplayType gameplayType = GameplayType.VR;
    public ControllerType controllerType = ControllerType.Touch;
     
    public GameObject deviceVRController;
    public GameObject gameOverGO;

    public bool IsGameOver = false;

    int _enemiesToNextLevel = 0;
    EnemyManager enemySpawner;

    public int EnemiesToNextLevel
    {
        get { return _enemiesToNextLevel; }
        set
        {
            _enemiesToNextLevel = value;
            UpdateEnemyCnt();
        }
    }

    void Start()
    {
        if (!enemySpawner) { enemySpawner = GetComponent<EnemyManager>(); }
        if (!Player) { Player = GameObject.FindWithTag("Player"); }
        if (!deviceVRController) { deviceVRController = GameObject.FindWithTag("GVRMain"); }

        DontDestroyOnLoad(this);

        InitNewGame();
    }

    void InitNewGame()
    {
        if (difficulty == DiffModifier.None)
        {
            difficulty = DiffModifier.Easy;
        }

        switch (difficulty)
        {
            case DiffModifier.Easy:
                EnemiesToNextLevel = 15;
                snowballDamageToEnemies = 35f;
                snowballDamageToPlayer = 5f;
                enemySpawner.SpawnTimer = 6f;
                enemySpawner.maxEnemiesInLevel = 3;
                break;

            case DiffModifier.Medium:
                EnemiesToNextLevel = 20;
                snowballDamageToEnemies = 30f;
                snowballDamageToPlayer = 10f;
                enemySpawner.SpawnTimer = 5f;
                enemySpawner.maxEnemiesInLevel = 5;
                break;

            case DiffModifier.Hard:
                EnemiesToNextLevel = 30;
                snowballDamageToEnemies = 25f;
                snowballDamageToPlayer = 15f;
                enemySpawner.SpawnTimer = 4f;
                enemySpawner.maxEnemiesInLevel = 8;
                break;
        }
    }

    void Update()
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
        IsGameOver = true;
        enemySpawner.DespawnAllEnemies();
        Time.timeScale = 0.3f;
        gameOverGO.SetActive(true);
    }

    public void SetGameplayType(GameplayType type)
    {
        switch (type)
        {
            case GameplayType.VR:
                deviceVRController.GetComponent<GvrViewer>().VRModeEnabled = true;
                break;

            case GameplayType.Gyro:
                deviceVRController.GetComponent<GvrViewer>().VRModeEnabled = false;
                break;
        }
    }

    public void SetDiff(DiffModifier diff)
    {
        difficulty = diff;
    }

    void UpdateEnemyCnt()
    {
        if (EnemiesToNextLevel == 0)
        {
            LoadNextLevel();
        }
    }

}
