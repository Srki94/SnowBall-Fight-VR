using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace snowman
{
    public class ScoreData
    {
        public int maxScore;
        public int lastScore;   // Last session score global
                                //  public int monstersKilled;
        public float longestSurvived;
        public int ballsHit;
        public int monstersHit;
        public int ballsThrown;
        public int sessionScore;

        public ScoreData()
        {
            ResetCounters();
        }

        public void ResetCounters()
        {
            maxScore = 0;
            lastScore = 0;
            //   monstersKilled = 0;
            longestSurvived = 0f;
            ballsHit = 0;
            monstersHit = 0;
            sessionScore = 0;
            ballsThrown = 0;
        }

        public void LoadScores()
        {
            maxScore = PlayerPrefs.GetInt("maxScore");
            lastScore = PlayerPrefs.GetInt("lastScore");
            // monstersKilled = PlayerPrefs.GetInt("monstersKilled");
            longestSurvived = PlayerPrefs.GetFloat("longestSurvived");
            ballsHit = PlayerPrefs.GetInt("ballsHit");
            monstersHit = PlayerPrefs.GetInt("monstersHit");
            ballsThrown = PlayerPrefs.GetInt("ballsThrown");
        }

        public void SaveScores()
        {
            PlayerPrefs.SetInt("maxScore", maxScore);
            PlayerPrefs.SetInt("lastScore", lastScore);
            //  PlayerPrefs.SetInt("monstersKilled", monstersKilled);
            PlayerPrefs.SetInt("ballsHit", ballsHit);
            PlayerPrefs.SetInt("monstersHit", monstersHit);
            PlayerPrefs.SetFloat("longestSurvived", longestSurvived);
            PlayerPrefs.SetInt("ballsThrown", ballsThrown);

        }

        public void DebugOutput()
        {

        }
    }
}