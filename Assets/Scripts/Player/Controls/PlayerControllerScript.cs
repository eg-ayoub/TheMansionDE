using InputManagement;
using Player.Movement;
namespace Player.Controls
{
    /// <summary>
    /// this script controls in-game controls of the player 
    /// </summary>
    public class PlayerControllerScript : GameplayObject
    {
        public bool jumpButtonDown;
        public bool jumpButtonUp;
        public float horizontal;
        /// <summary>
        /// player's movement modifier we send movement controls to
        /// </summary>
        PlayerMovementModifier movementModifier;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            movementModifier = PlayerInstanciationScript.movementModifier;
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if (!paused)
            {
                FetchOneTimeControls();
                FetchControls();
            }
        }
        /// <summary>
        ///! this is only for one time controls IE buttonUP buttonDOWN
        /// </summary>
        void FetchOneTimeControls()
        {
            if (KeyMapper.GetButtonDown("Jump")) jumpButtonDown = true;
            if (KeyMapper.GetButtonUp("Jump")) jumpButtonUp = true;
        }

        void FetchControls()
        {
            horizontal = KeyMapper.GetAxis("Horizontal");
            PlayerInstanciationScript.player.direction
                    = horizontal > 0
                    ? DIRECTION.RIGHT
                    : DIRECTION.LEFT;
        }

        public void ResetOneTimeControls()
        {
            jumpButtonDown = false;
            jumpButtonUp = false;
        }

    }
}