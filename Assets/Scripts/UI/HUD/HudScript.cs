using UnityEngine;
namespace UI.HUD
{
    /// <summary>
    /// this script manages the HUD and updates it on demand
    /// </summary>
    public class HudScript : MonoBehaviour
    {
        public static HudScript hud;
        HPDisplay hPDisplay;
        KeyStatusDisplay keyStatusDisplay;
        TimerDisplay timerDisplay;

        private void Awake()
        {
            if (hud == null)
            {
                hud = this;
                DontDestroyOnLoad(this);
            }
            else if (hud != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            hPDisplay = GetComponentInChildren<HPDisplay>();
            keyStatusDisplay = GetComponentInChildren<KeyStatusDisplay>();
            timerDisplay = GetComponentInChildren<TimerDisplay>();
        }

        public void UpdateHP(int HP)
        {
            hPDisplay.UpdateHP(HP);
        }

        public void UpdateKeyStatus(bool status)
        {
            keyStatusDisplay.UpdateKeyStatus(status);
        }

        public void UpdateTimer(int time)
        {
            timerDisplay.SetTime(time);
        }

        public void ExitHub()
        {
            timerDisplay.gameObject.SetActive(true);
            keyStatusDisplay.gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
        }

        public void EnterHub()
        {
            timerDisplay.gameObject.SetActive(false);
            keyStatusDisplay.gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }

    }
}