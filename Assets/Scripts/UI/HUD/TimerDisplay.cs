using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class TimerDisplay : MonoBehaviour
    {
        const int TICKS_IN_SECOND = 1000;
        const int SECONDS_IN_MINUTE = 60;
        const int MINUTES_IN_HOUR = 60;
        private const string Format = "{0}{1:D2}:{2:D2}.{3:D3}";
        Text text;

        private void Start()
        {
            text = GetComponent<Text>();
        }

        /// <summary>
        /// set the timer
        /// </summary>
        /// <param name="time">the number of milliseconds that have passed since the beginning of the level</param>
        public void SetTime(int time)
        {
            int milliseconds = time % TICKS_IN_SECOND;
            time = time / TICKS_IN_SECOND; // number of seconds elapsed
            int seconds = time % SECONDS_IN_MINUTE;
            time = time / SECONDS_IN_MINUTE; // number of minutes elapsed
            int minutes = time % MINUTES_IN_HOUR;
            time = time / MINUTES_IN_HOUR; // number of hours elapsed
            int hours = time;

            string display = string.Format(Format, hours != 0 ? hours + ":" : "", minutes, seconds, milliseconds);
            text.text = display;
        }
    }
}