using UnityEngine;
using UI.HUD;
using Management;
using System.Collections;
using CameraWorks;
namespace Player.Health
{
    // using State;

    /// <summary>
    /// manages player HP
    /// </summary>
    public class PlayerHealthManager : GameplayObject
    {
        bool immune;
        bool inMadness;
        /// <summary>
        /// current HP value
        /// </summary>
        [SerializeField] int HP;

        DeathEffector deathEffector;
        private void Start()
        {
            HP = Constants.PLAYER_START_HP;
            HudScript.hud.UpdateHP(HP);
            deathEffector = GetComponentInChildren<DeathEffector>();
        }
        // Update is called once per frame
        public void TakeDamage()
        {
            if (!immune)
            {
                immune = true;
                if (HP == 1)
                {
                    PlayerInstanciationScript.playerAudio.PlayDeathEffect();
                    GameManagerScript.gameManager.ReturnToMain(false);
                }
                else
                {
                    if (!inMadness)
                    {
                        HP -= 1;
                        // HudScript.hud.UpdateHP(HP);
                        HudScript.hud.UpdateHPLastBroken(HP);
                    }
                    // if (!paused)
                    // {
                    // }
                    GameManagerScript.gameManager.RestartLevel();
                }
                CameraShaker shaker = FindObjectOfType<CameraShaker>();
                if (shaker) shaker.Shake();
                deathEffector.Play();
                // StartCoroutine(ResetImmunity());
            }
        }

        public void ResetImmunity()
        {
            immune = false;
        }

        public void SetMadness(bool m)
        {
            Debug.Log("inMadness set to : " + m);
            inMadness = m;
            if (m)
            {
                HudScript.hud.MansionHP();
            }
            else
            {
                HudScript.hud.UpdateHP(HP);
            }
        }


        // IEnumerator ResetImmunity()
        // {
        //     for (int i = 0; i < 10; i++)
        //     {
        //         yield return null;
        //     }
        //     immune = false;
        //     yield return null;
        // }
        /// <summary>
        /// restores 'health' amount of points to player's HP
        /// </summary>
        /// <param name="health"> amount of HP to restore</param>
        public void DoHeal()
        {
            if (!paused)
            {
                HP = Mathf.Clamp(HP + 1, 0, Constants.PLAYER_MAX_HP);
                HudScript.hud.UpdateHP(HP);
            }

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
        public void ResetHP()
        {
            HP = Constants.PLAYER_MAX_HP;
            HudScript.hud.UpdateHP(HP);
        }

        public void ResetHud()
        {
            HudScript.hud.UpdateHP(HP);
        }

    }
}
