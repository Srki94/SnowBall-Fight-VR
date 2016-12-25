using UnityEngine;
using System.Collections;
namespace snowman
{
    public class ProjectileHelper : MonoBehaviour
    {
        public float damage = 35f;
        public bool CanDamage = true;

        void Start()
        {
            if (tag == "CPUAmmo")
            {
                damage = GameObject.FindWithTag("GameController").GetComponent<GameMgr>().snowballDamageToPlayer;
            }
            else if (tag == "PlayerAmmo")
            {
                damage = GameObject.FindWithTag("GameController").GetComponent<GameMgr>().snowballDamageToEnemies;
            }
        }

        void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.tag == "Terrain")
            {
                GameObject.Destroy(gameObject, 2f);
            }
            else if (collision.gameObject.tag == "CPUAmmo"
                && gameObject.tag == "PlayerAmmo")
            {
                GAMESESSION.SCORE.ballsHit++;
            }
        }
    }
}