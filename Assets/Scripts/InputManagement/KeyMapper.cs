using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InputManagement
{
    /// <summary>
    /// static library that manages mapping actual keys to the game's controls
    /// </summary>
    public class KeyMapper
    {
        public const int BUTTON_COUNT = 2;
        public const int AXIS_COUNT = 2;
        /// <summary>
        /// key map
        /// </summary>
        static Dictionary<string, ButtonMap> currentKeyMapping;
        /// <summary>
        /// axis map
        /// </summary>
        static Dictionary<string, AxisMap> currentAxisMapping;
        /// <summary>
        /// game's actions / controls
        /// </summary>
        static string[] actions = new string[BUTTON_COUNT]{
            "Jump", //Space
            "Pause" //Pause
		};

        /// <summary>
        /// game's axes
        /// </summary>
        static string[] axes = new string[AXIS_COUNT]{
            "Horizontal",
            "Vertical",
        };

        /// <summary>
        /// initializes the key map to map actions and axes to the default keyboard map
        /// </summary>
        private static void InitializeKeyMapper()
        {
            currentKeyMapping = new Dictionary<string, ButtonMap>();
            currentAxisMapping = new Dictionary<string, AxisMap>();
            MapAll(Mappings.kbButtons, Mappings.kbAxes);
        }
        /// <summary>
        /// static constructor
        /// </summary>
        static KeyMapper()
        {
            InitializeKeyMapper();
        }
        /// <summary>
        /// returns true on the frame where action's mapped key is pressed
        /// </summary>
        /// <param name="action">action to seek</param>
        /// <returns>bool : key has been pressed this frame</returns>
        public static bool GetButtonDown(string action)
        {
            return currentKeyMapping[action].ButtonDown();
        }
        /// <summary>
        /// returns true as long as action's assigned key key is pressed
        /// </summary>
        /// <param name="action">action to seek</param>
        /// <returns>bool : key is being pressed this frame</returns>
        public static bool GetButton(string action)
        {
            return currentKeyMapping[action].Button();
        }
        /// <summary>
        /// returns true on the frame where action's assigned  key is released
        /// </summary>
        /// <param name="action">action to seek</param>
        /// <returns>bool : key has been released this frame</returns>
        public static bool GetButtonUp(string action)
        {
            return currentKeyMapping[action].ButtonUp();
        }
        /// <summary>
        /// returns value of the real axis mapped to axis.
        /// </summary>
        /// <param name="axis">axis to seek</param>
        /// <returns>float : value of the axis</returns>
        public static float GetAxis(string axis)
        {
            return currentAxisMapping[axis].GetAxis();
        }

        /// <summary>
        /// applies a new mapping
        /// </summary>
        /// <param name="newKeys">new keycodes for the actions</param>
        /// <param name="newAxes">new axes</param>
        public static void MapAll(ButtonMap[] newKeys, AxisMap[] newAxes)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                if (newKeys[i] != null)
                {
                    currentKeyMapping[actions[i]] = newKeys[i];
                }
            }


            for (int i = 0; i < axes.Length; i++)
            {
                if (newAxes[i] != null)
                {
                    currentAxisMapping[axes[i]] = newAxes[i];
                }
            }
        }

        /// <summary>
        /// this method overrides controls for current frame
        /// </summary>
        public static void ResetAll()
        {
            Input.ResetInputAxes();
        }
    }
}
