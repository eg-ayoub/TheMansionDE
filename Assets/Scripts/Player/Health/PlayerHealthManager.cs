using System.Collections;
using UnityEngine;
namespace Player.Health
{
    // using State;
    using UI.HUD;

    /// <summary>
    /// manages player HP
    /// </summary>
    public class PlayerHealthManager : GameplayObject
    {
        /// <summary>
        /// whether the player is taking damage ( post - hit immunity)
        /// </summary>
        bool takingDamage;
        /// <summary>
        /// current HP value
        /// </summary>
        [SerializeField] int HP;
        /// <summary>
        /// timer before player can take damage again
        /// </summary>
        [SerializeField, Header("Post Hit Immunity")]
        int damageReturnTimer;
        private void Start()
        {
            takingDamage = true;
            HP = 3;
            HudScript.hud.UpdateHP(HP);
        }
        // Update is called once per frame
        public void TakeDamage(int damage)
        {
            if (!paused && takingDamage && damage >= 0)
            {
                if (damage >= HP)
                {
                    HP = 0;
                    // GameManagerScript.gameManager.ReturnToCheckPoint();
                    //wtf happens in this case
                }
                else
                {
                    HP -= damage;
                    StartCoroutine(DamageStop());
                }
                HudScript.hud.UpdateHP(HP);
            }
        }
        /// <summary>
        /// restores 'health' amount of points to player's HP
        /// </summary>
        /// <param name="health"> amount of HP to restore</param>
        public void DoHeal()
        {
            if (!paused)
            {
                HP = Mathf.Clamp(HP + 1, 0, Constants.Constants.PLAYER_MAX_HP);
                HudScript.hud.UpdateHP(HP);
            }

        }

        /// <summary>
        /// applies player HP immunity after hit
        /// </summary>
        IEnumerator DamageStop()
        {
            takingDamage = false;
            //PlayerInstanciationScript.player.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            for (int i = 0; i < damageReturnTimer; i++)
            {
                yield return 0;
            }
            takingDamage = true;
            //PlayerInstanciationScript.player.GetComponentInChildren<MeshRenderer>().material.color = Color.white;

        }
        /// <summary>
        /// getter for HP value
        /// </summary>
        public int GetHP()
        {
            return HP;
        }
        /// <summary>
        /// setter for HP value, used to load saved game
        /// </summary>
        /// <param name="health">new HP value</param>
        void SetHP(int health)
        {
            HP = Mathf.Clamp(health, 0, Constants.Constants.PLAYER_MAX_HP);
            HudScript.hud.UpdateHP(HP);
        }

    }
}
