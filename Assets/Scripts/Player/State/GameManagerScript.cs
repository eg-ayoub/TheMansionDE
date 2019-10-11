using UnityEngine;
// using DynamicScenes;
// using HUD;
// using Persistance;
// using Persistance.Serializables;
namespace Player.State
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
        // /// <summary>
        // /// variable to store current progression
        // /// </summary>
        // PersistantThings persistance;
        /// <summary>
        /// list of all the instanciated gameObjects ( used for pause)
        /// </summary>
        GameplayObject[] objects;
        /// <summary>
        /// global pause value.
        /// </summary>
        bool paused;
        bool pauseLocked;

        Transform playerTransform;
        // SceneManagerScript sceneManager;

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
                playerTransform = PlayerInstanciationScript.playerTransform;
                // if(persistance == null){
                //     persistance = new PersistantThings();
                // }
                // persistance.SceneIndex = 2;//TODO: change this later
                Application.targetFrameRate = 60;
                // sceneManager = SceneManagerScript.manager;
            }
        }



        // /**
        //     CHECKPOINTS AND SAVE POINTS            
        // */

        // /// <summary>
        // /// gets stats of the current game.
        // /// </summary>
        // /// <returns>returns : data about current game for saving</returns>
        // public PersistantThings GetStats()
        // {
        //     return persistance;
        // }

        // /// <summary>
        // /// sets stats for the current game.
        // /// </summary>
        // /// <param name="saved">stats read from a file</param>
        // public void SetStats(PersistantThings saved)
        // {
        //     persistance = saved;
        //     ApplyStats(false);
        // }

        // /// <summary>
        // /// applies current persistance stats
        // /// </summary>
        // void ApplyStats(bool checkpoint)
        // {

        //     if (!checkpoint)
        //     {
        //         PlayerInstanciationScript.hpManager.SendMessage("SetHP", persistance.HP_p);
        //         PlayerInstanciationScript.hpManager.SendMessage("SetMaxHP", persistance.maxHP_p);
        //         PlayerInstanciationScript.energyBroker.SendMessage("SetEnergy", persistance.energy_p);
        //     }

        //     sceneManager.SetCurrentScene(persistance.SceneIndex);
        //     playerTransform.position = new Vector3(persistance.x, persistance.y, persistance.z);
        // }

        // /// <summary>
        // /// called when player enters a checkpoint
        // /// </summary>
        // public void EnterCheckPoint()
        // {
        //     persistance.SceneIndex = sceneManager.CurrentSceneIndex;
        //     persistance.x = playerTransform.position.x;
        //     persistance.y = playerTransform.position.y;
        //     persistance.z = playerTransform.position.z;
        //     persistance.HP_p = PlayerInstanciationScript.hpManager.GetHP();
        //     persistance.maxHP_p = PlayerInstanciationScript.hpManager.GetMaxHP();
        //     persistance.energy_p = PlayerInstanciationScript.energyBroker.GetEnergy();
        //     PlayerInstanciationScript.conditionManager.EnterCheckpoint();
        // }

        // /// <summary>
        // /// Invoked after player receives damage from a deadzone 
        // /// </summary>
        // public void ReturnToCheckPoint()
        // {
        //     if (PlayerInstanciationScript.hpManager.GetHP() == 0)
        //     {
        //         //player has died
        //         ReturnToSavePoint();
        //         Debug.Log("death");
        //     }
        //     else
        //     {
        //         ApplyStats(true);
        //         sceneManager.LoadGame();
        //     }
        //     PlayerInstanciationScript.conditionManager.ReturnToCheckPoint();

        // }

        // /// <summary>
        // /// saves game at save point
        // /// </summary>
        // /// <example>
        // /// example:
        // /// <code>
        // /// private void OnTriggerEnter2D(Collider2D collision)
        // ///{
        // ///     GameManagerScript.gameManager.SaveGame();
        // ///}
        // /// </code>
        // /// </example>
        // public void SaveGame()
        // {
        //     EnterCheckPoint();
        //     SaveGameManager.manager.SaveGame();
        // }

        // /// <summary>
        // /// called when player reaches 0hp
        // /// </summary>
        // public void ReturnToSavePoint()
        // {
        //     SaveGameManager.manager.LoadGame();
        // }

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

        /**
            OVERRIDES PLAYER MOVEMENT
         */
        /// <summary>
        /// accesses the player's physics to freeze him temporarily
        /// </summary>
        public void FreezePlayer()
        {
            PlayerInstanciationScript.physics.Freeze();
        }

    }
}
