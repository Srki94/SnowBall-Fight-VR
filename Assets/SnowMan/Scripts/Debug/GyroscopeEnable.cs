﻿#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
namespace snowman
{
    public class GyroscopeEnable : MonoBehaviour
    {
        private Gyroscope gyro;

        void Start()
        {
            if (SystemInfo.supportsGyroscope)
            {
                gyro = Input.gyro;
                gyro.enabled = true;
            }
            DontDestroyOnLoad(this);
        }
    }
}
#endif