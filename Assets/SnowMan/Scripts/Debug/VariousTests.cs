﻿using UnityEngine;
using System.Collections;
namespace snowman
{
    public class VariousTests : MonoBehaviour
    {

        // Use this for initialization
        void Awake()
        {
            //   GAMESESSION.SCORE.LoadScores();

        }

        // Update is called once per frame
        void Start()
        {
            var f = gameObject.GetComponent<GvrAudioSource>();
        }
    }
}