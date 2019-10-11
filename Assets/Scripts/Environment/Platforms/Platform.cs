using UnityEngine;
namespace Environment.Platforms
{
    /// <summary>
    /// parent class for all the platform types
    /// </summary>
    public abstract class Platform : GameplayObject
    {
        /// <summary>
        /// whether the player is on the platform
        /// </summary>
        protected bool playerOn;

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !paused)
            {
                playerOn = true;
                PlayerOn();
            }
        }
        protected void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !paused)
            {
                playerOn = false;
                PlayerOff();
            }
        }
        /// <summary>
        /// action to execute when player gets on the platform
        /// </summary>
        public abstract void PlayerOn();
        /// <summary>
        /// action to execute when the player gets off the platform
        /// </summary>
        public abstract void PlayerOff();


    }
}