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

        /// <summary>
        /// choses when the manager can be active again
        /// </summary>
        bool carry;

        AxisButtonUpdater axisButtonUpdater;


        /*
         * KEYBOARD MAPPING GOES HERE
         *
         *   
         */
        /// <summary>
		/// default keyboard key assignments
		/// </summary>
		public static ButtonMap[] kbButtons = new ButtonMap[KeyMapper.BUTTON_COUNT]{
            new KeyCodeButtonMap(KeyCode.E),
            new KeyCodeButtonMap(KeyCode.LeftShift),
            new KeyCodeButtonMap(KeyCode.Q),
            new KeyCodeButtonMap(KeyCode.Space),
            new KeyCodeButtonMap(KeyCode.V),
            new KeyCodeButtonMap(KeyCode.T),
            new KeyCodeButtonMap(KeyCode.M),
            new KeyCodeButtonMap(KeyCode.Escape),
            new KeyCodeButtonMap(KeyCode.N),
            new KeyCodeButtonMap(KeyCode.B),
            new KeyCodeButtonMap(KeyCode.R)
        };

        /// <summary>
        /// default keyboard axes
        /// </summary>
        public static AxisMap[] kbAxes = new AxisMap[KeyMapper.AXIS_COUNT]{
            new AxisMap("KBH"),
            new AxisMap("KBV"),
            new AxisMap("KBCAM")
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
        public static Dictionary<string, ButtonMap> XBoxOneAllButtons
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
            XBoxOneAllButtons["X"], // X -> slap, 
            XBoxOneAllButtons["LT"], // RT -> magic
            XBoxOneAllButtons["Y"], // Y -> interact
            XBoxOneAllButtons["A"], // A -> jump
            XBoxOneAllButtons["B"], // B -> dash
            XBoxOneAllButtons["HD"],// down arrow -> heal
            XBoxOneAllButtons["SELECT"], // select -> map
            XBoxOneAllButtons["START"], // start -> pause 
            XBoxOneAllButtons["RB"], // RB -> next
            XBoxOneAllButtons["LB"], // LB  -> previous
            XBoxOneAllButtons["RT"] //LT -> throw scepter
        };

        /// <summary>
        /// dictionary storing all xbox one controller axes.
        /// can be used for user remapping
        /// </summary>
        /// <typeparam name="string">axis name</typeparam>
        /// <typeparam name="AxisMap">axis mapping </typeparam>
        /// <returns></returns>
        public static Dictionary<string, AxisMap> XBoxOneAllAxes
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
            XBoxOneAllAxes["LSV"],
            XBoxOneAllAxes["RSV"]
        };



        /// <summary>
        /// states of the input 
        /// </summary>
        enum Mode { controller, kb };

        /// <summary>
        /// actually using keyboard or controller ?
        /// </summary>
        Mode thisMode = Mode.kb;

        /// <summary>
        /// switch to keyboard or controller ? 
        /// </summary>
        Mode nextMode = Mode.kb;

        /// <summary>
        /// applies xbox one mapping
        /// </summary>
        public void MapXBONE()
        {
            Debug.Log("switching to xbox one mapping");
            KeyMapper.MapAll(XBoxOneButtons, XBoxAxes);
            carry = true;
        }
        /// <summary>
        /// switches to user appointed mapping for keyboard
        /// </summary>
        public void MapUserController()
        {
            Debug.Log("switching to user controller mapping");
            KeyMapper.MapUserController();
            carry = true;
        }
        /// <summary>
        /// switches to user appointed mapping for controller
        /// </summary>
        public void MapUserKB()
        {
            Debug.Log("switching to user keyboard mapping");
            KeyMapper.MapUserKB();
            carry = true;
        }
        /// <summary>
        /// switches to default keyboard mapping
        /// </summary>
        public void MapKB()
        {
            Debug.Log("switching to default keyboard mapping");
            KeyMapper.ResetKeyMapper();
            carry = true;
        }
        void Start()
        {
            StartCoroutine(SwitchController());
            carry = true;
            axisButtonUpdater = GetComponent<AxisButtonUpdater>();

            foreach (ButtonMap button in XBoxOneAllButtons.Values)
            {
                if (button is AxisButtonMap)
                {
                    axisButtonUpdater.AddButton((AxisButtonMap)button);
                }
            }
        }

        // void Update() {
        //     if(carry){

        //         if(thisMode == Mode.kb){
        //             for(int i = 0; i < 20; i++){
        //                 string b = "JoystickButton" + i;
        //                 if(Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), b))){
        //                     nextMode = Mode.controller;
        //                 }
        //             }  
        //         }

        //         if(thisMode == Mode.controller){
        //             foreach(KeyCode key in KeyMapper.kbKeys){
        //                 if(Input.GetKey(key)){
        //                     nextMode = Mode.kb;
        //                 }
        //             }
        //         }
        //     }

        // }
        /// <summary>
        /// switches to last used controller's mapping if there is a change 
        /// </summary>
        IEnumerator SwitchController()
        {
            yield return new WaitForSecondsRealtime(.5f);

            if (nextMode != thisMode)
            {
                carry = false;
                switch (nextMode)
                {
                    case Mode.kb:
                        GameManagerScript.gameManager.ToggleGamePaused();
                        //!HudScript.hud.keyBoardChoice.SetActive(true);
                        while (!carry)
                        {
                            yield return null;
                        }
                        break;
                    case Mode.controller:
                        GameManagerScript.gameManager.ToggleGamePaused();
                        //!HudScript.hud.ControllerChoice.SetActive(true);
                        while (!carry)
                        {
                            yield return null;
                        }
                        break;
                }
            }

            thisMode = nextMode;
            StopAllCoroutines();
            StartCoroutine(SwitchController());
        }
    }
}