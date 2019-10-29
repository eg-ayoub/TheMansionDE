using UnityEngine;
using UI.HUD;
using Management;
namespace Player.Health
{
    // using State;

    /// <summary>
    /// manages player HP
    /// </summary>
    public class PlayerHealthManager : GameplayObject
    {
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
            if (HP == 1)
            {
                GameManagerScript.gameManager.ReturnToMain();
            }
            else
            {
                HP -= 1;
                GameManagerScript.gameManager.RestartLevel();
            }

            if (!paused)
            {
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
        public void SetHP(int health)
        {
            HP = Mathf.Clamp(health, 0, Constants.PLAYER_MAX_HP);
            HudScript.hud.UpdateHP(HP);
        }

    }
}
