using System.Collections.Generic;
using UnityEngine;
namespace InputManagement
{
    public static class Mappings
    {
        /*
         * KEYBOARD MAPPING INFO
         * GOES HERE
         */
        public static ButtonMap[] kbButtons = new ButtonMap[KeyMapper.BUTTON_COUNT]{
            new KeyCodeButtonMap(KeyCode.Space),
            new KeyCodeButtonMap(KeyCode.Escape),
            new KeyCodeButtonMap(KeyCode.UpArrow)
        };
        public static AxisMap[] kbAxes = new AxisMap[KeyMapper.AXIS_COUNT]{
            new AxisMap("KBH"),
            new AxisMap("KBV")
    };

        /*
         * XBOX ONE CONTROLLER MAPPING INFO 
         * GOES HERE
         * STICK DIRECTIONS AS BUTTONS NOT SUPPORTED INTENTIONALLY 
         * DPAD AS AXES NOT SUPPORTED INTENTIONALLY      
         */

        /// <summary>
        /// Dictionary storing all possible xbox one controller button assignments. can be
        /// used for user remapping
        /// </summary>
        /// <typeparam name="string"> button name </typeparam>
        /// <typeparam name="ButtonMap"> button map</typeparam>
        /// <returns></returns>
        static Dictionary<string, ButtonMap> XBoxOneAllButtons
                        = new Dictionary<string, ButtonMap>(){
            {"A" , new KeyCodeButtonMap(KeyCode.JoystickButton0)},
            {"B" , new KeyCodeButtonMap(KeyCode.JoystickButton1)},
            {"X" , new KeyCodeButtonMap(KeyCode.JoystickButton2)},
            {"Y" , new KeyCodeButtonMap(KeyCode.JoystickButton3)},
            {"LB" , new KeyCodeButtonMap(KeyCode.JoystickButton4)},
            {"RB" , new KeyCodeButtonMap(KeyCode.JoystickButton5)},
            {"SELECT" , new KeyCodeButtonMap(KeyCode.JoystickButton6)},
            {"START" , new KeyCodeButtonMap(KeyCode.JoystickButton7)},
            {"LSTICK", new KeyCodeButtonMap(KeyCode.JoystickButton8)},
            {"RSTICK", new KeyCodeButtonMap(KeyCode.JoystickButton9)},
            {"LT", new AxisButtonMap("XBOX-LT",1)},
            {"RT", new AxisButtonMap("XBOX-RT",1)},
            {"HU", new AxisButtonMap("XBOX-HV",1)},
            {"HD", new AxisButtonMap("XBOX-HV",-1)},
            {"HL", new AxisButtonMap("XBOX-HH",-1)},
            {"HR", new AxisButtonMap("XBOX-HH",1)},
        };

        /// <summary>
        /// default mapping for xbox one controller keys
        /// </summary>
        public static ButtonMap[] XBoxOneButtons = new ButtonMap[KeyMapper.BUTTON_COUNT]{
            XBoxOneAllButtons["A"], // A -> jump
            XBoxOneAllButtons["START"], //START -> pause
            XBoxOneAllButtons["HU"] //HAT UP -> enter level
        };

        /// <summary>
        /// dictionary storing all xbox one controller axes.
        /// can be used for user remapping
        /// </summary>
        /// <typeparam name="string">axis name</typeparam>
        /// <typeparam name="AxisMap">axis mapping </typeparam>
        /// <returns></returns>
        static Dictionary<string, AxisMap> XBoxOneAllAxes
                        = new Dictionary<string, AxisMap>(){
            {"LSH", new AxisMap("XBOX-LSH")},
            {"LSV", new AxisMap("XBOX-LSV")},
            {"RSH", new AxisMap("XBOX-RSH")},
            {"RSV", new AxisMap("XBOX-RSV")},
            {"LT", new AxisMap("XBOX-LT")},
            {"RT", new AxisMap("XBOX-RT")}
        };

        /// <summary>
        /// mapping for joystick axes
        /// </summary>
        public static AxisMap[] XBoxAxes = new AxisMap[KeyMapper.AXIS_COUNT]{
            XBoxOneAllAxes["LSH"],
            XBoxOneAllAxes["LSV"]
        };

        /// <summary>
        /// Dictionary storing all possible xbox one controller button assignments. can be
        /// used for user remapping
        /// </summary>
        /// <typeparam name="string"> button name </typeparam>
        /// <typeparam name="ButtonMap"> button map</typeparam>
        /// <returns></returns>
        static Dictionary<string, ButtonMap> DS4AllButtons
                        = new Dictionary<string, ButtonMap>(){
            {"Cross" , new KeyCodeButtonMap(KeyCode.JoystickButton1)},
            {"Square" , new KeyCodeButtonMap(KeyCode.JoystickButton0)},
            {"Triangle" , new KeyCodeButtonMap(KeyCode.JoystickButton3)},
            {"Circle" , new KeyCodeButtonMap(KeyCode.JoystickButton2)},
            {"L1" , new KeyCodeButtonMap(KeyCode.JoystickButton4)},
            {"R1" , new KeyCodeButtonMap(KeyCode.JoystickButton5)},
            {"SELECT" , new KeyCodeButtonMap(KeyCode.JoystickButton8)},
            {"START" , new KeyCodeButtonMap(KeyCode.JoystickButton9)},
            {"L3", new KeyCodeButtonMap(KeyCode.JoystickButton10)},
            {"R3", new KeyCodeButtonMap(KeyCode.JoystickButton11)},
            {"L2", new KeyCodeButtonMap(KeyCode.JoystickButton6)},
            {"R2", new KeyCodeButtonMap(KeyCode.JoystickButton7)},
            {"HU", new AxisButtonMap("DS4-HV",1)},
            {"HD", new AxisButtonMap("DS4-HV",-1)},
            {"HL", new AxisButtonMap("DS4-HH",-1)},
            {"HR", new AxisButtonMap("DS4-HH",1)},
        };

        /// <summary>
        /// dictionary storing all xbox one controller axes.
        /// can be used for user remapping
        /// </summary>
        /// <typeparam name="string">axis name</typeparam>
        /// <typeparam name="AxisMap">axis mapping </typeparam>
        /// <returns></returns>
        static Dictionary<string, AxisMap> DS4AllAxes
                        = new Dictionary<string, AxisMap>(){
            {"LSH", new AxisMap("DS4-LSH")},
            {"LSV", new AxisMap("DS4-LSV")},
            {"RSH", new AxisMap("DS4-RSH")},
            {"RSV", new AxisMap("DS4-RSV")},
            {"L2", new AxisMap("DS4-LT")},
            {"R2", new AxisMap("DS4-RT")}
        };

        /// <summary>
        /// default mapping for ds4  controller keys
        /// </summary>
        public static ButtonMap[] DS4Buttons = new ButtonMap[KeyMapper.BUTTON_COUNT]{
            DS4AllButtons["Cross"], // Cross -> jump
            DS4AllButtons["START"], //START -> pause
            DS4AllButtons["HU"]
        };

        /// <summary>
        /// mapping for ds4 joystick axes
        /// </summary>
        public static AxisMap[] DS4Axes = new AxisMap[KeyMapper.AXIS_COUNT]{
            DS4AllAxes["LSH"],
            DS4AllAxes["LSV"]
        };

        public static void UpdaterSetup(AxisButtonUpdater updater, JoypadChoser.mapping map)
        {
            updater.Reset();
            switch (map)
            {
                case JoypadChoser.mapping.XBONE:
                    foreach (ButtonMap button in XBoxOneAllButtons.Values)
                    {
                        if (button is AxisButtonMap)
                        {
                            updater.AddButton((AxisButtonMap)button);
                        }
                    }
                    break;
                case JoypadChoser.mapping.DS4:
                    foreach (ButtonMap button in DS4AllButtons.Values)
                    {
                        if (button is AxisButtonMap)
                        {
                            updater.AddButton((AxisButtonMap)button);
                        }
                    }
                    break;
            }

        }
    }

}