using UnityEngine;
namespace Environment.Platforms
{
    /// <summary>
    /// this platform disappears when the player leaves them
    /// </summary>
    public class VanishingPlatform : Platform
    {
        public override void PlayerOff()
        {
            Vanish();
        }

        public override void PlayerOn()
        {
            // ! do nothing
        }

        void Vanish()
        {
            Destroy(gameObject);
        }
    }
}