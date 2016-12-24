using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SnowmanAnnouncerController : MonoBehaviour
{

    public GameObject[] GameOverUIElements;
    public GameObject[] NewLevelUIElements;
    public enum AnnouncerType { Gameover, NewLevel }
    public AnnouncerType thisAnnouncertype;
    public Text countdownText;
    public Text diffTxt;
    public Text levelName;

    GameMgr GameMgr;

    Animator thisAnimator;

    void Awake()
    {
        thisAnimator = GetComponent<Animator>();
        GameMgr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMgr>();
    }
    

    public void SetActiveElement(AnnouncerType type)
    {
        if (type == AnnouncerType.Gameover)
        {
            foreach (GameObject go in NewLevelUIElements)
            {
                go.SetActive(false);
            }

            foreach (GameObject go in GameOverUIElements)
            {
                go.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject go in GameOverUIElements)
            {
                go.SetActive(false);
            }

            foreach (GameObject go in NewLevelUIElements)
            {
                go.SetActive(true);
            }

            string tmp = "";
            if (GameMgr.currLevelID == 1)
            {
                tmp = "One";
            }
            else
            {
                tmp = "Two";
            }
            levelName.text = "Level " + tmp;
        }

        thisAnimator.SetTrigger("Spawn");
        diffTxt.text = GameMgr.difficulty.ToString();
    }

    public void UpdateDiffText()
    {
        diffTxt.text = GameMgr.difficulty.ToString();
    }

   

    public void UpdateCountdownText(string txt)
    {
        countdownText.text = txt;
    }

    public void Despawn()
    {
        thisAnimator.SetTrigger("Despawn");
    }

   // public void Spawn()
   // {
   //     thisAnimator.SetTrigger("Spawn");
   // }
   //

}
