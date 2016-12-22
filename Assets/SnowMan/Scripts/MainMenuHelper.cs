using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHelper : MonoBehaviour
{
    int gameModeID = 0; // 0 VR 1 Gyro
    int gameDiff = 0; // 0 Easy 1 Medium 2 Hard
    int controllerType = 0; // 0 Touch 1 Magnet 2 Auto

    public Toggle vrTog;
    public Toggle gyrTog;


    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void SetGameMode(int id)
    {
        gameModeID = id;
    }

    public void SetGameDiff(int id)
    {
        gameDiff = id;
    }

    public void SetGameControllerType(int id)
    {
        controllerType = id;
    }

    public void StartNewGame()
    { 
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        GameMgr gmgr = GameObject.FindWithTag("GameController").GetComponent<GameMgr>();
        if (!gmgr) { Debug.Log("Couldn't find game manager"); }

        GAMESESSION.GAMEPLAY_TYPE = (GameMgr.GameplayType)gameModeID;

       gmgr.SetGameplayType((GameMgr.GameplayType)gameModeID);
       gmgr.SetDiff((GameMgr.DiffModifier)gameDiff);
       gmgr.SetControllerType((GameMgr.ControllerType)controllerType);
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        GameObject.Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
       //SceneManager.sceneLoaded
    }
}
