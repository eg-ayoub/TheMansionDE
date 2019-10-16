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
        LevelDisplay levelDisplay;
        KeyStatusDisplay keyStatusDisplay;
        TimerDisplay timerDisplay;

        private void Awake()
        {
            if (hud == null)
            {
                hud = this;
            }
            else if (hud != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            hPDisplay = GetComponentInChildren<HPDisplay>();
            levelDisplay = GetComponentInChildren<LevelDisplay>();
            keyStatusDisplay = GetComponentInChildren<KeyStatusDisplay>();
            timerDisplay = GetComponentInChildren<TimerDisplay>();
        }

        public void UpdateHP(int HP)
        {
            hPDisplay.UpdateHP(HP);
        }

        public void UpdateLevel(int level)
        {
            levelDisplay.UpdateLevel(level);
        }

        public void UpdateKeyStatus(bool status)
        {
            keyStatusDisplay.UpdateKeyStatus(status);
        }

        public void UpdateTimer(int time)
        {
            timerDisplay.SetTime(time);
        }

    }
}