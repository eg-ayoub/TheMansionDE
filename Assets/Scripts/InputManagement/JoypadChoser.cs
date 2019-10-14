using System;
using System.Collections;
using System.Collections.Generic;
using Player.State;
using UnityEngine;
namespace InputManagement
{
    /// <summary>
    /// this script automatizes the joystick switching when a new joystick is being used
    /// </summary>
    public class JoypadChoser : MonoBehaviour
    {
        List<string> joystickNames = new List<string>();
        /// <summary>
        /// applies xbox one mapping
        /// </summary>
        void MapXBONE()
        {
            Debug.Log("switching to xbox one mapping");
            KeyMapper.MapAll(Mappings.XBoxOneButtons, Mappings.XBoxAxes);
        }

        /// <summary>
        /// switches to default keyboard mapping
        /// </summary>
        void MapKB()
        {
            Debug.Log("switching to default keyboard mapping");
            KeyMapper.MapAll(Mappings.kbButtons, Mappings.kbAxes);
        }

        /// <summary>
        /// applies ps4 mapping
        /// </summary>
        void MapDS4()
        {
            Debug.Log("switching to DS4 mapping");
            KeyMapper.MapAll(Mappings.DS4Buttons, Mappings.DS4Axes);
        }

        void Start()
        {
            Mappings.UpdaterSetup(GetComponent<AxisButtonUpdater>());
        }

        void Update()
        {
            List<string> newJoystickNames = new List<string>(Input.GetJoystickNames());
            foreach (string name in newJoystickNames)
            {
                if (!joystickNames.Contains(name))
                {
                    // * new joystick detected
                    Debug.Log(name + " : connected !");
                    // * some logic about which mapping to choose
                }
            }
            if (newJoystickNames.Count == 0 && joystickNames.Count != 0)
            {
                MapKB();
            }
            joystickNames = newJoystickNames;
        }
    }
}