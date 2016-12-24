using SnowMan.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    public enum DiffModifier { Easy, Medium, Hard, None };
    public enum GameplayType { VR = 0, Gyro = 1 };
    public enum ControllerType { Touch = 0, Magnet = 1, Auto = 2 }

    public GameObject Player;
    public float enemyShootTimer = 0f;
    public float enemySpawnTimer = 0f;
    public int currScore = 0;
    public float snowballDamageToEnemies = 0f;
    public float snowballDamageToPlayer = 0f;
    public DiffModifier difficulty = DiffModifier.None;
    public GameplayType gameplayType = GameplayType.VR;
    public ControllerType controllerType = ControllerType.Touch;
   // public static ScoreData playerScoreData = new ScoreData();

    public GameObject deviceVRController;
    public GameObject announcerSnowmanGO;
    public int currLevelID = 1;

    public bool IsGameOver = false;

    int _enemiesToNextLevel = 0;
    EnemyManager enemySpawner;
    float sessionPlayerAlive = 0f;

    public int EnemiesToNextLevel
    {
        get { return _enemiesToNextLevel; }
        set
        {
            _enemiesToNextLevel = value;
            UpdateEnemyCnt();
        }
    }

    private void Awake()
    {
        GameObject[] test = GameObject.FindGameObjectsWithTag("GameController");

        if (test.Length > 1)
        {
            Destroy(this);
        }
    }

    void Start()
    {

        if (!enemySpawner) { enemySpawner = GetComponent<EnemyManager>(); }
        if (!Player) { Player = GameObject.FindWithTag("Player"); }
        if (!deviceVRController) { deviceVRController = GameObject.FindWithTag("GVRMain"); }

        //DontDestroyOnLoad(this);
        //SetGameplayType(GameplayType.Gyro);
        SetControllerType(GAMESESSION.controllerType);
        SetDiff(GAMESESSION.difficulty);
        SetGameplayType(GAMESESSION.gameplayType);
        InitNewGame();
    }

    void Update()
    {
        if (!IsGameOver)
        {
            sessionPlayerAlive += 1f * Time.deltaTime;
        }
    }

    void InitNewGame(bool reloading = false)
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
        if (reloading) { announcerSnowmanGO = GameObject.FindGameObjectWithTag("Announcer"); }
        announcerSnowmanGO.GetComponent<SnowmanAnnouncerController>()
            .SetActiveElement(SnowmanAnnouncerController.AnnouncerType.NewLevel);


    }

    public void LoadNextLevel()
    {
        Debug.Log("Called next level");
    }

    public void ResetCurrLevel()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Reconfig();
        Debug.Log(EnemiesToNextLevel + " enemies to next lvl GMR");
    }

    void Reconfig()
    {
        SetGameplayType(gameplayType, true);
        SetDiff(difficulty);
        SetControllerType(controllerType);
        InitNewGame(true);
        enemySpawner.RemoveAllEnemies();
    }

    public void NextDiff()
    {
        if (difficulty == DiffModifier.Easy)
        {
            SetDiff(DiffModifier.Medium);
        }
        else if (difficulty == DiffModifier.Medium)
        {
            SetDiff(DiffModifier.Hard);
        }
        else if (difficulty == DiffModifier.Hard)
        {
            SetDiff(DiffModifier.Easy);
        }
        announcerSnowmanGO.GetComponent<SnowmanAnnouncerController>().UpdateDiffText();
    }

    public void InitGameOver()
    {
        IsGameOver = true;
        HandleScore();
        enemySpawner.DespawnAllEnemies();
        announcerSnowmanGO.SetActive(true);
        announcerSnowmanGO.GetComponent<SnowmanAnnouncerController>()
            .SetActiveElement(SnowmanAnnouncerController.AnnouncerType.Gameover);
    }

    void HandleScore()
    {
        if (sessionPlayerAlive > GAMESESSION.SCORE.longestSurvived)
        {
            GAMESESSION.SCORE.longestSurvived = sessionPlayerAlive;
        }
        GAMESESSION.SCORE.lastScore = GAMESESSION.SCORE.sessionScore;
        GAMESESSION.SCORE.sessionScore = 0;

        GAMESESSION.SCORE.SaveScores();

        sessionPlayerAlive = 0f;
    }

    public void InitLvlComplete()
    {
        HandleScore();
        enemySpawner.DespawnAllEnemies();
        announcerSnowmanGO.SetActive(true); // Hack : Make waves on the same map ... 
        announcerSnowmanGO.GetComponent<SnowmanAnnouncerController>().SetActiveElement(SnowmanAnnouncerController.AnnouncerType.NewLevel);
    }

    public void SetGameplayType(GameplayType type, bool sceneReload = false)
    {
        GAMESESSION.gameplayType = type;

        if (sceneReload)
        {
            deviceVRController = GameObject.FindGameObjectWithTag("GVRMain");
        }
        switch (type)
        {
            case GameplayType.VR:
                deviceVRController.GetComponent<GvrViewer>().VRModeEnabled = true;
                gameplayType = GameplayType.VR;
                break;

            case GameplayType.Gyro:
                deviceVRController.GetComponent<GvrViewer>().VRModeEnabled = false;
                gameplayType = GameplayType.Gyro;
                break;
        }
    }

    public void SetDiff(DiffModifier diff)
    {
        GAMESESSION.difficulty = diff;
        difficulty = diff;
    }

    public void SetControllerType(ControllerType type)
    {
        GAMESESSION.controllerType = type;
        controllerType = type;
    }
    void UpdateEnemyCnt()
    {
        if (EnemiesToNextLevel == 0)
        {
            LoadNextLevel();
        }
    }

}
