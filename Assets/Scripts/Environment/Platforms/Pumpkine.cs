using UnityEngine;
using Player;
using Player.Movement;
namespace Environment.Platforms
{
    public class Pumpkine : Platform
    {
        Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        const float pinnacle = 1000.0f;

        public override void PlayerOff()
        {
            // ! do nothing
        }

        public override void PlayerOn()
        {
            animator.SetTrigger("Bounce");
        }

        public void BouncePlayer()
        {
            PlayerInstanciationScript.playerTransform.Translate(300 * Vector3.up);
            PlayerInstanciationScript.movementModifier.TrampolineJump(pinnacle);
        }


    }
}