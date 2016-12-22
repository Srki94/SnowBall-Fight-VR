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

    Animator thisAnimator;

    private void Awake()
    {
        thisAnimator = GetComponent<Animator>();
    }

    public void SetActiveElement(AnnouncerType type)
    {
        if (type == AnnouncerType.Gameover)
        {
            foreach(GameObject go in NewLevelUIElements)
            {
                go.SetActive(false);
            }

            foreach(GameObject go in GameOverUIElements)
            {
                go.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject go in NewLevelUIElements)
            {
                go.SetActive(true);
            }

            foreach (GameObject go in GameOverUIElements)
            {
                go.SetActive(false);
            }
        }

        thisAnimator.SetTrigger("Spawn");
        diffTxt.text = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMgr>().difficulty.ToString();
    }

    public void UpdateCountdownText(string txt)
    {
        countdownText.text = txt;
    }

 
}
