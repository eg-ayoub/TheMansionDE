using UnityEngine;
namespace Environment.Platforms
{
    /// <summary>
    /// this platform disappears when the player leaves them
    /// </summary>
    public class VanishingPlatform : Platform
    {
        Animator animator;
        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        public override void PlayerOff()
        {
            animator.SetTrigger("Vanish");
        }

        public override void PlayerOn()
        {
            // ! do nothing
        }

        public void Vanish()
        {
            Destroy(gameObject);
        }
    }
}