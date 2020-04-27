using UnityEngine;
using InputManagement;
using System.Collections;
using UnityEngine.SceneManagement;
using Player;
using UI.Loading;
using UI.HUD;
using UI.PauseMenu;
using Player.Audio;
using Cinemachine;
namespace Management
{
    using System;
    using System.Threading;
    using Serialization;
    /// <summary>
    /// Game Manager is a singleton class that manages all aspects of the game.
    /// </summary>
    public class GameManagerScript : MonoBehaviour
    {
        public bool isLoading;
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

        int collectiblesInRun;

        internal void IncrementCollectibles()
        {
            collectiblesInRun++;
        }

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

        internal void LeaveGame()
        {
            // what do we do before leaving game ?
            Application.Quit();
        }

        void Start()
        {
            if (gameManager == this)
            {
                Application.targetFrameRate = 60;
            }
            ToggleGamePaused();
            timer = GetComponentInChildren<Timer>();
            saveManager = GetComponentInChildren<SaveManager>();
            currentHandle = FindObjectOfType<LevelHandle>();
        }

        private void Update()
        {
            if (KeyMapper.GetButtonDown("Pause") && currentHandle.buildIndex != 0)
            {
                ToggleGamePaused();
                if (paused) PauseMenu.menu.Show();
                else PauseMenu.menu.Hide();
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

        public void ReturnToMain(bool fromMenu)
        {
            if (!isLoading)
            {
                StartCoroutine(HubReturn(fromMenu));
                isLoading = true;
            }
        }

        public void RestartLevel()
        {
            if (!isLoading)
            {
                StartCoroutine(LevelRestart());
                isLoading = true;
            }
        }

        public void Enter(int target, bool mansion)
        {
            if (!isLoading)
            {
                StartCoroutine(EnterLevel(target, mansion));
                isLoading = true;
            }
        }

        public void Next()
        {
            if (!isLoading)
            {
                StartCoroutine(PreNextLevel());
                isLoading = true;
            }
        }

        IEnumerator WaitForPause()
        {
            while (paused)
            {
                yield return null;
            }
        }

        IEnumerator EnterLevel(int index, bool mansion)
        {
            // * 1 - pause all gameObjects
            yield return StartCoroutine(WaitForPause());
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

            // * 5 - put player in spawnpoint and reset sensors
            PlayerInstanciationScript.playerTransform.position = currentHandle.spawnpoint.position;
            PlayerInstanciationScript.movementModifier.ResetSensors();
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

            // * 7b - play audio
            if (mansion)
            {
                PlayerInstanciationScript.playerAudio.SetClip(PlayerAudioSource.CLIP.MADNESS);
            }
            else
            {
                PlayerInstanciationScript.playerAudio.SetClip(PlayerAudioSource.CLIP.MANSION);
            }
            PlayerInstanciationScript.playerAudio.Play();

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
            PlayerInstanciationScript.movementModifier.ResetSensors();
            PlayerInstanciationScript.hpManager.ResetImmunity();
            PlayerInstanciationScript.hpManager.SetMadness(mansion);
            isLoading = false;
        }

        IEnumerator HubReturn(bool fromMenu)
        {
            // * 1 - pause all gameObjects
            yield return StartCoroutine(WaitForPause());
            ToggleGamePaused();
            LockPause();
            yield return null;

            // * 2 - play first part of the loading animation (hides the screen)
            LoadingOverlay.overlay.Play(LoadingOverlay.ANIMATIONS.DEATH);
            while (!LoadingOverlay.overlay.isIdle)
            {
                yield return null;
            }

            if (!fromMenu) LoadingOverlay.overlay.DisplayGameOver();

            // * 2b pause audio
            PlayerInstanciationScript.playerAudio.Pause();

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
            PlayerInstanciationScript.movementModifier.ResetSensors();
            yield return null;

            // * 5b - reset player HP
            PlayerInstanciationScript.hpManager.ResetHP();

            // * reset collectibles
            collectiblesInRun = 0;

            // * 5c - delay
            if (!fromMenu) yield return new WaitForSeconds(4f);
            LoadingOverlay.overlay.RemoveIndicators();
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
            PlayerInstanciationScript.player.Sleep();
            // FindObjectOfType<CinemachineVirtualCamera>().Follow = PlayerInstanciationScript.playerTransform;
            // // * stop timer (we don't need it in hubworld)
            // timer.StopTimer();
            PlayerInstanciationScript.movementModifier.ResetSensors();
            PlayerInstanciationScript.hpManager.ResetImmunity();
            PlayerInstanciationScript.hpManager.SetMadness(false);
            isLoading = false;
        }

        IEnumerator LevelRestart()
        {
            // * 1 - pause all gameObjects
            yield return StartCoroutine(WaitForPause());
            ToggleGamePaused();
            LockPause();
            yield return null;

            // * 2 - play first part of the loading animation (hides the screen)
            LoadingOverlay.overlay.Play(LoadingOverlay.ANIMATIONS.RESTART);
            while (!LoadingOverlay.overlay.isIdle)
            {
                yield return null;
            }

            PlayerInstanciationScript.playerTransform.position = currentHandle.spawnpoint.position;

            // * 3 - wait for scene to load
            AsyncOperation levelLoad = SceneManager.LoadSceneAsync(currentHandle.buildIndex, LoadSceneMode.Single);
            while (!levelLoad.isDone)
            {
                yield return null;
            }

            // * 4 - query new scene handle
            currentHandle = FindObjectOfType<LevelHandle>();
            yield return null;

            // * 5 - put player in spawnpoint and reset collisions
            PlayerInstanciationScript.movementModifier.ResetSensors();
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

            // * 7b set some UI stuff
            HudScript.hud.UpdateKeyStatus(false);
            PlayerInstanciationScript.hpManager.ResetHud();

            // * 8 - freeze player and reset controls for 10 frames
            PlayerInstanciationScript.clipManager.Freeze();
            for (int _ = 0; _ < 10; _++)
            {
                KeyMapper.ResetAll();
                yield return null;
            }
            PlayerInstanciationScript.clipManager.UnFreeze();
            PlayerInstanciationScript.movementModifier.ResetSensors();
            PlayerInstanciationScript.hpManager.ResetImmunity();
            isLoading = false;

        }

        IEnumerator PreNextLevel()
        {

            // * 1 - pause all gameObjects
            yield return StartCoroutine(WaitForPause());
            ToggleGamePaused();
            LockPause();
            yield return null;

            // * 2 is this GameOver ? 
            if (currentHandle is MadnessHandle)
            {
                MadnessHandle handle = (MadnessHandle)currentHandle;
                yield return StartCoroutine(saveManager.CheckIsGameOver(handle.checkpoint));
                if (saveManager.GetIsGameOver())
                {
                    yield return StartCoroutine(GameOver());
                }
                else
                {
                    yield return StartCoroutine(NextLevel());
                }
            }
            else
            {
                yield return StartCoroutine(NextLevel());
            }

            // * 8 resume all gameObjects
            UnLockPause();
            ToggleGamePaused();
            yield return null;


            // * 9 set some UI stuff
            HudScript.hud.UpdateKeyStatus(false);

            // * 10 freeze player and reset controls for 10 frames
            PlayerInstanciationScript.clipManager.Freeze();
            for (int _ = 0; _ < 10; _++)
            {
                KeyMapper.ResetAll();
                yield return null;
            }
            PlayerInstanciationScript.clipManager.UnFreeze();
            if (currentHandle.buildIndex == 0) PlayerInstanciationScript.player.Sleep();
            PlayerInstanciationScript.movementModifier.ResetSensors();
            PlayerInstanciationScript.hpManager.ResetImmunity();
            PlayerInstanciationScript.hpManager.SetMadness(false);
            isLoading = false;

        }

        IEnumerator NextLevel()
        {

            // * 3 - play first part of the loading animation (hides the screen)
            if (!currentHandle.isCheckpoint)
            {
                LoadingOverlay.overlay.Play(LoadingOverlay.ANIMATIONS.NEXT_LEVEL);
            }
            else
            {
                LoadingOverlay.overlay.Play(LoadingOverlay.ANIMATIONS.HUB_ON_SUCCESS);
                PlayerInstanciationScript.playerAudio.PlayWinEffect();
            }

            while (!LoadingOverlay.overlay.isIdle)
            {
                yield return null;
            }

            if (currentHandle.isCheckpoint)
                LoadingOverlay.overlay.DisplayWin();

            // * 4a - which scene do I load ?
            int nextLevel = currentHandle.isCheckpoint ? 0 : currentHandle.buildIndex + 1;

            // * 4b - save the game if checkpoint
            if (currentHandle.isCheckpoint)
            {
                int time = timer.GetTime();
                if (currentHandle is NormalHandle)
                {
                    NormalHandle handle = (NormalHandle)currentHandle;
                    yield return StartCoroutine(saveManager.SaveCoroutineNormal(handle.checkpoint, timer.GetTime(), collectiblesInRun));
                }
                else
                {
                    MadnessHandle handle = (MadnessHandle)currentHandle;
                    yield return StartCoroutine(saveManager.SaveCoroutineMadness(handle.checkpoint, timer.GetTime(), collectiblesInRun));
                }
                collectiblesInRun = 0;
            }

            // * pause audio
            if (nextLevel == 0) PlayerInstanciationScript.playerAudio.Pause();

            // * 4c - wait for scene to load
            AsyncOperation levelLoad = SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Single);
            while (!levelLoad.isDone)
            {
                yield return null;
            }

            // * 5 - query new scene handle
            currentHandle = FindObjectOfType<LevelHandle>();
            yield return null;

            // * 6 - put player in spawnpoint
            PlayerInstanciationScript.playerTransform.position = currentHandle.spawnpoint.position;
            PlayerInstanciationScript.movementModifier.ResetSensors();

            yield return null;

            // * 6b - reset player HP 
            PlayerInstanciationScript.hpManager.ResetHP();
            if (nextLevel == 0)
            {
                yield return new WaitForSeconds(2f);
            }
            LoadingOverlay.overlay.RemoveIndicators();

            // * 7 - play second part of loading animation (shows screen)
            LoadingOverlay.overlay.Resume();
            while (!LoadingOverlay.overlay.isDone)
            {
                yield return null;
            }
            LoadingOverlay.overlay.Reset();

        }

        IEnumerator GameOver()
        {
            // * 3 play 1st part of the loading anim
            LoadingOverlay.overlay.Play(LoadingOverlay.ANIMATIONS.GAME_OVER);

            while (!LoadingOverlay.overlay.isIdle)
            {
                yield return null;
            }

            // * 4a save
            MadnessHandle handle = (MadnessHandle)currentHandle;
            yield return StartCoroutine(saveManager.SaveCoroutineMadness(handle.checkpoint, timer.GetTime(), collectiblesInRun));
            collectiblesInRun = 0;

            // * 4b mute audio
            PlayerInstanciationScript.playerAudio.Pause();

            // * 4c wait for scene to load
            AsyncOperation levelLoad = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
            while (!levelLoad.isDone)
            {
                yield return null;
            }

            // * 4d wait for cinematic
            while (!LoadingOverlay.overlay.idlingDone)
            {
                yield return null;
            }

            // * 5 - query new scene handle
            currentHandle = FindObjectOfType<LevelHandle>();
            yield return null;

            // * 6 - put player in spawnpoint
            PlayerInstanciationScript.playerTransform.position = currentHandle.spawnpoint.position;
            PlayerInstanciationScript.movementModifier.ResetSensors();

            yield return null;

            // * 6b - reset player HP 
            PlayerInstanciationScript.hpManager.ResetHP();
            yield return new WaitForSeconds(2f);
            LoadingOverlay.overlay.RemoveIndicators();

            // * 7 - play second part of loading animation (shows screen)
            LoadingOverlay.overlay.Resume();
            while (!LoadingOverlay.overlay.isDone)
            {
                yield return null;
            }
            LoadingOverlay.overlay.Reset();

        }

    }
}
