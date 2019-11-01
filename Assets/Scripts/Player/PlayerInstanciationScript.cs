using UnityEngine;
namespace Player
{
    using Health;
    // using State;
    using Movement;
    using Graphics;
    using Controls;
    using Audio;
    /// <summary>
    /// player singleton, ensures there is only ever one player in the scene
    /// </summary>
    public class PlayerInstanciationScript : MonoBehaviour
    {
        /// <summary>
        /// static singleton player object, used to refer to the player gameObject
        /// </summary>
        /// <example>
        /// example :
        /// <code>
        /// PlayerInstanciationScript.player.SendMessage(message);
        /// </code>
        /// </example>
        public static PlayerInstanciationScript player;
        /// <summary>
        /// direction the player is looking at
        /// </summary>
        public DIRECTION direction;


        public static PlayerHealthManager hpManager;
        public static PlayerPhysics physics;
        public static PlayerMovementModifier movementModifier;
        public static PlayerClipManager clipManager;
        public static Transform playerTransform;
        public static PlayerGraphicsManager graphicsManager;
        public static PlayerControllerScript playerController;
        public static PlayerAudioSource playerAudio;


        private void Awake()
        {
            if (player == null)
            {
                DontDestroyOnLoad(gameObject);
                player = this;
            }
            else if (player != this)
            {
                //player.transform.position = gameObject.transform.position;
                Destroy(gameObject);
                //player.GetComponentInChildren<PlayerPhysics>().Freeze();
            }
        }
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            if (this == player)
            {
                playerTransform = player.transform;
                hpManager = player.GetComponentInChildren<PlayerHealthManager>();
                physics = player.GetComponentInChildren<PlayerPhysics>();
                movementModifier = player.GetComponentInChildren<PlayerMovementModifier>();
                clipManager = player.GetComponentInChildren<PlayerClipManager>();
                graphicsManager = player.GetComponentInChildren<PlayerGraphicsManager>();
                playerController = player.GetComponentInChildren<PlayerControllerScript>();
                playerAudio = player.GetComponentInChildren<PlayerAudioSource>();
            }
        }

    }
}