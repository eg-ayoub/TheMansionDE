using UnityEngine;
using UI.HUD;

namespace Management
{
    public class Timer : GameplayObject
    {
        /// <summary>
        /// number of milliseconds since start of level
        /// </summary>
        int time = 0;
        int now;

        private void Start()
        {
            StartTimer();
        }

        public void StartTimer()
        {
            now = (int)(1000 * Time.time);
            time = 0;
        }

        public int GetTime()
        {
            return time;
        }

        private void FixedUpdate()
        {
            int deltaTime = (int)(1000 * Time.time) - now;
            now = (int)(1000 * Time.time);
            if (!paused)
            {
                time += deltaTime;
                HudScript.hud.UpdateTimer(time);
            }
        }
    }
}