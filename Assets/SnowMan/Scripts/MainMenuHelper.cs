using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHelper : MonoBehaviour
{
    int gameModeID = 0; // 0 VR 1 Gyro

    public Toggle vrTog;
    public Toggle gyrTog;


    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void SetGameMode()
    {
        if (vrTog.isOn)
        {
            gameModeID = 0;
        }
        else if (gyrTog.isOn)
        {
            gameModeID = 1;
        }
        else
        {
            gameModeID = 1;
        }
        Debug.Log(gameModeID);
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
        
        GameObject.Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
       //SceneManager.sceneLoaded
    }
}
