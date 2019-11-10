using UnityEngine;
using Management;
using Player;
namespace UI.PauseMenu
{
    public class PauseMenu : MonoBehaviour
    {
        public static PauseMenu menu;
        PauseMenuInputModule inputModule;

        private void Awake()
        {
            if (menu == null)
            {
                menu = this;
                DontDestroyOnLoad(this);
            }
            else if (menu != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            inputModule = GetComponentInChildren<PauseMenuInputModule>();
        }

        public void Show()
        {
            inputModule.Active();
            transform.GetChild(0).gameObject.SetActive(true);
        }

        public void Hide()
        {
            inputModule.Inactive();
            transform.GetChild(0).gameObject.SetActive(false);
        }

        public void Retry()
        {
            GameManagerScript.gameManager.ToggleGamePaused();
            Hide();
            PlayerInstanciationScript.hpManager.TakeDamage();
        }
        public void Quit()
        {
            GameManagerScript.gameManager.LeaveGame();
        }
        public void Resume()
        {
            GameManagerScript.gameManager.ToggleGamePaused();
            Hide();
        }

        public void HubReturn()
        {
            GameManagerScript.gameManager.ToggleGamePaused();
            Hide();
            GameManagerScript.gameManager.ReturnToMain(true);
        }
    }
}