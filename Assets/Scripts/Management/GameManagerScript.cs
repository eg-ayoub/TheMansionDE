using UnityEngine;
using InputManagement;
// using DynamicScenes;
// using HUD;
// using Persistance;
// using Persistance.Serializables;
namespace Management
{
    /// <summary>
    /// Game Manager is a singleton class that manages all aspects of the game.
    /// </summary>
    public class GameManagerScript : MonoBehaviour
    {

        /// <summary>
        /// game manager singleton
        /// </summary>
        public static GameManagerScript gameManager;
        /// <summary>
        /// list of all the instanciated gameObjects ( used for pause)
        /// </summary>
        GameplayObject[] objects;
        /// <summary>
        /// global pause value.
        /// </summary>
        bool paused;
        bool pauseLocked;


        void Awake()
        {
            /*persistance: does this bother loading game ? */
            paused = false;
            if (gameManager == null)
            {
                DontDestroyOnLoad(gameObject);
                gameManager = this;
            }
            else if (gameManager != this)
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            if (gameManager == this)
            {
                Application.targetFrameRate = 60;
            }
        }

        private void Update()
        {
            if (KeyMapper.GetButtonDown("Pause"))
            {
                ToggleGamePaused();
            }
        }

        /**
            PAUSE STATE 
        */
        /// <summary>
        /// pauses or unpauses the game
        /// </summary>
        public void ToggleGamePaused()
        {
            if (!pauseLocked)
            {
                objects = FindObjectsOfType<GameplayObject>();
                paused = !paused;
                foreach (GameplayObject go in objects)
                {
                    if (paused) go.OnPauseGame();
                    else go.OnResumeGame();
                }
            }
        }

        public void LockPause()
        {
            pauseLocked = true;
        }

        public void UnLockPause()
        {
            pauseLocked = false;
        }

    }
}
