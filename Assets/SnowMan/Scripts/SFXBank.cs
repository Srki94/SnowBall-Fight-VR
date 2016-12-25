using UnityEngine;
using System.Collections.Generic;
namespace snowman
{
    public class SFXBank : MonoBehaviour
    {

        [System.Serializable]
        public class SFXBankEntry
        {
            public string sfxName;
            public int sfxID;
            public AudioClip sfxClip;
        }

        [SerializeField]
        List<SFXBankEntry> _SFXBank = new List<SFXBankEntry>();

        void Start()
        {
            if (_SFXBank.Count == 0)
            {
                Debug.LogError("SFX Bank is empty");
            }
        }

        public AudioClip GetSFX(string sID)
        {
            return _SFXBank.Find(x => x.sfxName == sID).sfxClip;
        }

        public AudioClip GetSFX(int sID)
        {
            return _SFXBank.Find(x => x.sfxID == sID).sfxClip;
        }

    }
}