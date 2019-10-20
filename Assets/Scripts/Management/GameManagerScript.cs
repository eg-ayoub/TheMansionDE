using UnityEngine;
using InputManagement;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using Player;
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

        // ? persistence
        LevelHandle currentHandle;


        void Awake()
        {
            /*persistance: does this bother loading game ? */
            paused = true;
            if (gameManager == null)
            {
                currentHandle = FindObjectOfType<LevelHandle>();
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
            ToggleGamePaused();
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

        internal void ReturnToCheckPoint()
        {
            throw new NotImplementedException();
        }

        internal void RestartLevel()
        {
            StartCoroutine(LevelRestart());
        }

        IEnumerator LevelRestart()
        {
            // * 1 - pause all gameObjects
            ToggleGamePaused();
            LockPause();
            yield return null;

            // * 2 - wait for scene to load
            AsyncOperation levelLoad = SceneManager.LoadSceneAsync(currentHandle.buildIndex, LoadSceneMode.Single);
            while (!levelLoad.isDone)
            {
                yield return null;
            }

            // * 3 - query new scene handle
            currentHandle = FindObjectOfType<LevelHandle>();
            yield return null;

            // * 4 - put player in spawnpoint
            PlayerInstanciationScript.playerTransform.position = currentHandle.spawnpoint.position;
            yield return null;

            // * 5 - resume all gameObjects
            UnLockPause();
            ToggleGamePaused();
            yield return null;

            // * 6 - freeze player and reset controls for 10 frames
            PlayerInstanciationScript.clipManager.Freeze();
            for (int _ = 0; _ < 10; _++)
            {
                KeyMapper.ResetAll();
                yield return null;
            }
            PlayerInstanciationScript.clipManager.UnFreeze();

            // * 7 - restart timer
            // ? mmm ? 
        }

    }
}
