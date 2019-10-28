using UnityEngine;
using InputManagement;
using System.Collections;
using UnityEngine.SceneManagement;
using Player;
using UI.Loading;
using UI.HUD;
namespace Management
{
    using Serialization;
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

        Timer timer;

        SaveManager saveManager;


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
#if UNITY_EDITOR
            ToggleGamePaused();
#endif
            timer = GetComponentInChildren<Timer>();
            saveManager = GetComponentInChildren<SaveManager>();
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

        public void ReturnToMain()
        {
            StartCoroutine(HubReturn());
        }

        public void RestartLevel()
        {
            StartCoroutine(LevelRestart());
        }

        public void Enter(int target)
        {
            StartCoroutine(EnterLevel(target));
        }

        public void Next()
        {
            StartCoroutine(NextLevel());
        }

        IEnumerator EnterLevel(int index)
        {
            // * 1 - pause all gameObjects
            ToggleGamePaused();
            LockPause();
            yield return null;

            // * 2 - play first part of the loading animation (hides the screen)
            LoadingOverlay.overlay.Play(LoadingOverlay.ANIMATIONS.LEVEL_ENTRY);
            while (!LoadingOverlay.overlay.isIdle)
            {
                yield return null;
            }

            // * 3 - wait for scene to load
            AsyncOperation levelLoad = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
            while (!levelLoad.isDone)
            {
                yield return null;
            }

            // * 4 - query new scene handle
            currentHandle = FindObjectOfType<LevelHandle>();
            yield return null;

            // * 5 - put player in spawnpoint
            PlayerInstanciationScript.playerTransform.position = currentHandle.spawnpoint.position;
            yield return null;

            // * 6 - play second part of loading animation (shows screen)
            LoadingOverlay.overlay.Resume();
            while (!LoadingOverlay.overlay.isDone)
            {
                yield return null;
            }
            LoadingOverlay.overlay.Reset();

            // * 7 - resume all gameObjects
            UnLockPause();
            ToggleGamePaused();
            yield return null;

            // * 8 - freeze player and reset controls for 10 frames
            PlayerInstanciationScript.clipManager.Freeze();
            for (int _ = 0; _ < 10; _++)
            {
                KeyMapper.ResetAll();
                yield return null;
            }
            PlayerInstanciationScript.clipManager.UnFreeze();

            // * 9 - set HUD to levelMode
            HudScript.hud.ExitHub();
            timer.StartTimer();
        }

        IEnumerator HubReturn()
        {
            // * 1 - pause all gameObjects
            ToggleGamePaused();
            LockPause();
            yield return null;

            // * 2 - play first part of the loading animation (hides the screen)
            LoadingOverlay.overlay.Play(LoadingOverlay.ANIMATIONS.GAME_OVER);
            while (!LoadingOverlay.overlay.isIdle)
            {
                yield return null;
            }

            // * 3 - wait for scene to load
            AsyncOperation levelLoad = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
            while (!levelLoad.isDone)
            {
                yield return null;
            }

            // * 4 - query new scene handle
            currentHandle = FindObjectOfType<LevelHandle>();
            yield return null;

            // * 5 - put player in spawnpoint
            PlayerInstanciationScript.playerTransform.position = currentHandle.spawnpoint.position;
            yield return null;

            // * 5b - reset player HP
            PlayerInstanciationScript.hpManager.SetHP(Constants.PLAYER_START_HP);

            // * 6 - play second part of loading animation (shows screen)
            LoadingOverlay.overlay.Resume();
            while (!LoadingOverlay.overlay.isDone)
            {
                yield return null;
            }
            LoadingOverlay.overlay.Reset();

            // * 7 - resume all gameObjects
            UnLockPause();
            ToggleGamePaused();
            yield return null;

            // * 8 - freeze player and reset controls for 10 frames
            PlayerInstanciationScript.clipManager.Freeze();
            for (int _ = 0; _ < 10; _++)
            {
                KeyMapper.ResetAll();
                yield return null;
            }
            PlayerInstanciationScript.clipManager.UnFreeze();
            // // * stop timer (we don't need it in hubworld)
            // timer.StopTimer();
        }

        IEnumerator LevelRestart()
        {
            // * 1 - pause all gameObjects
            ToggleGamePaused();
            LockPause();
            yield return null;

            // * 2 - play first part of the loading animation (hides the screen)
            LoadingOverlay.overlay.Play(LoadingOverlay.ANIMATIONS.RESTART);
            while (!LoadingOverlay.overlay.isIdle)
            {
                yield return null;
            }

            // * 3 - wait for scene to load
            AsyncOperation levelLoad = SceneManager.LoadSceneAsync(currentHandle.buildIndex, LoadSceneMode.Single);
            while (!levelLoad.isDone)
            {
                yield return null;
            }

            // * 4 - query new scene handle
            currentHandle = FindObjectOfType<LevelHandle>();
            yield return null;

            // * 5 - put player in spawnpoint
            PlayerInstanciationScript.playerTransform.position = currentHandle.spawnpoint.position;
            yield return null;

            // * 6 - play second part of loading animation (shows screen)
            LoadingOverlay.overlay.Resume();
            while (!LoadingOverlay.overlay.isDone)
            {
                yield return null;
            }
            LoadingOverlay.overlay.Reset();

            // * 7 - resume all gameObjects
            UnLockPause();
            ToggleGamePaused();
            yield return null;

            // * 8 - freeze player and reset controls for 10 frames
            PlayerInstanciationScript.clipManager.Freeze();
            for (int _ = 0; _ < 10; _++)
            {
                KeyMapper.ResetAll();
                yield return null;
            }
            PlayerInstanciationScript.clipManager.UnFreeze();
        }

        IEnumerator NextLevel()
        {
            // * 1 - pause all gameObjects
            ToggleGamePaused();
            LockPause();
            yield return null;

            // * 2 - play first part of the loading animation (hides the screen)
            if (currentHandle.checkpoint == 0)
                LoadingOverlay.overlay.Play(LoadingOverlay.ANIMATIONS.NEXT_LEVEL);
            else
                LoadingOverlay.overlay.Play(LoadingOverlay.ANIMATIONS.HUB_ON_SUCCESS);

            while (!LoadingOverlay.overlay.isIdle)
            {
                yield return null;
            }

            // * 3a - which scene do I load ?
            int nextLevel = currentHandle.checkpoint != 0 ? 0 : currentHandle.buildIndex + 1;

            // * 3b - save the game if checkpoint
            if (currentHandle.checkpoint != 0)
            {
                int time = timer.GetTime();
                yield return StartCoroutine(saveManager.SaveCoroutine(currentHandle.checkpoint, timer.GetTime()));
            }

            // * 3c - wait for scene to load
            AsyncOperation levelLoad = SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Single);
            while (!levelLoad.isDone)
            {
                yield return null;
            }

            // * 4 - query new scene handle
            currentHandle = FindObjectOfType<LevelHandle>();
            yield return null;

            // * 5 - put player in spawnpoint
            PlayerInstanciationScript.playerTransform.position = currentHandle.spawnpoint.position;
            yield return null;

            // * 5b - reset player HP if returning to hub
            if (nextLevel == 0)
                PlayerInstanciationScript.hpManager.SetHP(Constants.PLAYER_START_HP);

            // * 6 - play second part of loading animation (shows screen)
            LoadingOverlay.overlay.Resume();
            while (!LoadingOverlay.overlay.isDone)
            {
                yield return null;
            }
            LoadingOverlay.overlay.Reset();

            // * 7 - resume all gameObjects
            UnLockPause();
            ToggleGamePaused();
            yield return null;

            // * 7b set some UI stuff
            HudScript.hud.UpdateKeyStatus(false);
            HudScript.hud.UpdateLevel(currentHandle.buildIndex);

            // * 8 - freeze player and reset controls for 10 frames
            PlayerInstanciationScript.clipManager.Freeze();
            for (int _ = 0; _ < 10; _++)
            {
                KeyMapper.ResetAll();
                yield return null;
            }
            PlayerInstanciationScript.clipManager.UnFreeze();
        }

    }
}
