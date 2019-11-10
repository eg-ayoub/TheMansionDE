using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace InputManagement
{
    public class AxisButtonUpdater : MonoBehaviour
    {
        List<AxisButtonMap> buttons = new List<AxisButtonMap>();

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            foreach (AxisButtonMap button in buttons)
            {
                button.Update();
            }
        }

        public void AddButton(AxisButtonMap button)
        {
            if (!buttons.Contains(button))
            {
                buttons.Add(button);
            }
        }

        public void Reset()
        {
            buttons.Clear();
        }
    }
}