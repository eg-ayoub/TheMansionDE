using InputManagement;
using Player.Movement;
namespace Player.Controls
{
    /// <summary>
    /// this script controls in-game controls of the player 
    /// </summary>
    public class PlayerControllerScript : GameplayObject
    {
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

                movementModifier.SetControls(
                    KeyMapper.GetButtonUp("Jump"),
                    KeyMapper.GetButtonDown("Jump"),
                    KeyMapper.GetAxis("Horizontal"),
                    KeyMapper.GetAxis("Vertical")
                );

                PlayerInstanciationScript.player.direction
                    = KeyMapper.GetAxis("Horizontal") > 0
                    ? Constants.DIRECTION.RIGHT
                    : Constants.DIRECTION.LEFT;

            }
        }

    }
}