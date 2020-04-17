using UnityEngine;
using Player;
using Player.Movement;
namespace Environment.Platforms
{
    public class Pumpkine : Platform
    {

        AudioSource audioSource;

        Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }
        const float pinnacle = 3000.0f;

        public override void PlayerOff()
        {
            // ! do nothing
        }

        public override void PlayerOn()
        {
            animator.SetTrigger("Bounce");
            audioSource.Play();
        }

        public void BouncePlayer()
        {
            PlayerInstanciationScript.playerTransform.Translate(300 * Vector3.up);
            PlayerInstanciationScript.movementModifier.TrampolineJump(pinnacle);
        }


    }
}