using System;
using System.Collections;
using System.Collections.Generic;
// using Player.State;
using UnityEngine;
namespace InputManagement
{
    /// <summary>
    /// this script automatizes the joystick switching when a new joystick is being used
    /// </summary>
    public class JoypadChoser : MonoBehaviour
    {
        public enum mapping { KB = 0, DS4, XBONE };

        List<string> joystickNames = new List<string>();

        // public bool forceDebugMapping;
        // public mapping debugMapping;
        /// <summary>
        /// applies xbox one mapping
        /// </summary>
        void MapXBONE()
        {
            Debug.Log("switching to xbox one mapping");
            KeyMapper.MapAll(Mappings.XBoxOneButtons, Mappings.XBoxAxes);
            Mappings.UpdaterSetup(GetComponent<AxisButtonUpdater>(), mapping.XBONE);
        }

        /// <summary>
        /// switches to default keyboard mapping
        /// </summary>
        void MapKB()
        {
            Debug.Log("switching to default keyboard mapping");
            KeyMapper.MapAll(Mappings.kbButtons, Mappings.kbAxes);
            Mappings.UpdaterSetup(GetComponent<AxisButtonUpdater>(), mapping.KB);
        }

        /// <summary>
        /// applies ps4 mapping
        /// </summary>
        void MapDS4()
        {
            Debug.Log("switching to DS4 mapping");
            KeyMapper.MapAll(Mappings.DS4Buttons, Mappings.DS4Axes);
            Mappings.UpdaterSetup(GetComponent<AxisButtonUpdater>(), mapping.DS4);
        }

        void Start()
        {
            MapKB();
        }

        void Update()
        {
            // List<string> newJoystickNames = new List<string>(Input.GetJoystickNames());
            // foreach (string name in newJoystickNames)
            // {
            //     if (!joystickNames.Contains(name) && !forceDebugMapping)
            //     {
            //         Debug.Log("new controller detected : " + name);
            //         DetectAndRemap(name);
            //     }
            // }
            // if (newJoystickNames.Count == 0 && joystickNames.Count != 0)
            // {
            //     MapKB();
            // }
            // joystickNames = newJoystickNames;
        }

        // void DetectAndRemap(string name)
        // {
        //     switch (Application.platform)
        //     {
        //         case RuntimePlatform.WindowsEditor:
        //         case RuntimePlatform.WindowsPlayer:
        //             DetectAndRemapWindows(name);
        //             break;
        //         case RuntimePlatform.LinuxEditor:
        //         case RuntimePlatform.LinuxPlayer:
        //             DetectAndRemapLinux(name);
        //             break;
        //         case RuntimePlatform.OSXEditor:
        //         case RuntimePlatform.OSXPlayer:
        //             DetectAndRemapOSX(name);
        //             break;
        //         default:
        //             break;

        //     }
        // }
        // void DetectAndRemapWindows(string name)
        // {
        //     // TODO : fill me !
        // }
        // void DetectAndRemapLinux(string name)
        // {
        //     // TODO : fill me !
        // }
        // void DetectAndRemapOSX(string name)
        // {
        //     // TODO : fill me !
        // }

    }
}