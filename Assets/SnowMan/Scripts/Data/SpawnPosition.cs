using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.SnowMan.Scripts.Data
{
    [Serializable()]
 public   class SpawnPosition
    {
        public string ID;
        public Transform locationPosition;
        bool taken = false;

        public bool isEmpty()
        {
            return taken;
        }

         
    }
}
