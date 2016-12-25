using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace snowman
{
    public class SnowmanAnnouncerController : MonoBehaviour
    {

        public GameObject[] GameOverUIElements;
        public GameObject[] NewLevelUIElements;
        public GameObject[] LevelCompleteUIElements;

        public enum AnnouncerType
        {
            Gameover, NewLevel,
            LevelCompleted
        }
        public AnnouncerType thisAnnouncertype;
        public Text countdownText;
        public Text diffTxt;
        public Text diffTxt2;
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
            foreach (GameObject go in NewLevelUIElements)
            {
                go.SetActive(type == AnnouncerType.NewLevel ? true : false);
            }

            foreach (GameObject go in GameOverUIElements)
            {
                go.SetActive(type == AnnouncerType.Gameover ? true : false);
            }

            foreach (GameObject go in LevelCompleteUIElements)
            {
                go.SetActive(type == AnnouncerType.LevelCompleted ? true : false);
            }
            // if (type == AnnouncerType.Gameover)
            // {
            //     foreach (GameObject go in NewLevelUIElements)
            //     {
            //         go.SetActive(false);
            //     }
            //
            //     foreach (GameObject go in GameOverUIElements)
            //     {
            //         go.SetActive(true);
            //     }
            // }
            // else if (type == AnnouncerType.NewLevel)
            // {
            //     foreach (GameObject go in GameOverUIElements)
            //     {
            //         go.SetActive(false);
            //     }
            //
            //     foreach (GameObject go in NewLevelUIElements)
            //     {
            //         go.SetActive(true);
            //     }
            //
            //     string tmp = "";
            //     if (GameMgr.currLevelID == 1)
            //     {
            //         tmp = "One";
            //     }
            //     else
            //     {
            //         tmp = "Two";
            //     }
            //     levelName.text = "Level " + tmp;
            // }
            // else if (type == AnnouncerType.LevelCompleted)
            // {
            //     
            // }

            thisAnimator.SetTrigger("Spawn");
            diffTxt.text = GameMgr.difficulty.ToString();
            diffTxt2.text = GameMgr.difficulty.ToString();
        }

        public void UpdateDiffText()
        {
            diffTxt.text = GameMgr.difficulty.ToString();
            diffTxt2.text = GameMgr.difficulty.ToString();
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
}