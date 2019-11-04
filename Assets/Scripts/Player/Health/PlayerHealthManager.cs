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
        /// <summary>
        /// current HP value
        /// </summary>
        [SerializeField] int HP;
        private void Start()
        {
            HP = Constants.PLAYER_START_HP;
            HudScript.hud.UpdateHP(HP);
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
                    HP -= 1;
                    if (!paused)
                    {
                        HudScript.hud.UpdateHP(HP);
                    }
                    GameManagerScript.gameManager.RestartLevel();
                }
                CameraShaker shaker = FindObjectOfType<CameraShaker>();
                if (shaker) shaker.Shake();
                StartCoroutine(ResetImmunity());
            }
        }

        IEnumerator ResetImmunity()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return null;
            }
            immune = false;
            yield return null;
        }
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

    }
}
