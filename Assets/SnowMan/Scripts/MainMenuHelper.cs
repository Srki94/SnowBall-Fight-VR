using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHelper : MonoBehaviour
{
    int gameModeID = 0; // 0 VR 1 Gyro
    int gameDiff = 0; // 0 Easy 1 Medium 2 Hard

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

    public void StartNewGame()
    { // wait for level to load -> configure game manager -> destroy self
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        GameMgr gmgr = GameObject.FindWithTag("GameController").GetComponent<GameMgr>();
        if (!gmgr) { Debug.Log("Couldn't find game manager"); }

       gmgr.SetGameplayType((GameMgr.GameplayType)gameModeID);
       gmgr.SetDiff((GameMgr.DiffModifier)gameDiff);

        GameObject.Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
       //SceneManager.sceneLoaded
    }
}
