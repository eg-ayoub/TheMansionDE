using System.Collections;
using UnityEngine;
namespace Player.Health
{
    using State;
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
        /// <summary>
        /// max HP bar capacity
        /// </summary>
        int maxHP;

        bool healing;
        bool interrupt;

        [SerializeField] int popoyaTimer;
        private void Start()
        {
            takingDamage = true;
            maxHP = 7;
            HP = 5;
            //!HudScript.hud.UpdateHP(HP, maxHP);
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
                // Interrupt();
                //!HudScript.hud.UpdateHP(HP, maxHP);
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
                HP = Mathf.Clamp(HP + 1, 0, maxHP);
                //!HudScript.hud.UpdateHP(HP, maxHP);
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
            HP = Mathf.Clamp(health, 0, maxHP);
            //!HudScript.hud.UpdateHP(HP, maxHP);
        }
        /// <summary>
        /// setter for maxHP, used to load saved game
        /// </summary>
        /// <param name="maxhealth">new maxHP value</param>
        void SetMaxHP(int maxhealth)
        {
            maxHP = Mathf.Clamp(maxhealth, 1, 20);
            //!HudScript.hud.UpdateHP(HP, maxHP);
        }
        /// <summary>
        /// getter for maxHP value
        /// </summary>
        public int GetMaxHP()
        {
            return maxHP;
        }

    }
}
