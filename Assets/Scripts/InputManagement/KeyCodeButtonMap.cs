using UnityEngine;
namespace InputManagement
{
    public class KeyCodeButtonMap : ButtonMap{

        KeyCode code;
        public KeyCodeButtonMap(KeyCode code){
            this.code = code;
        }

        protected override bool GetDown(){
            return Input.GetKeyDown(code);
        }

        protected override bool GetUp(){
            return Input.GetKeyUp(code);
        }

        protected override bool Get(){
            return Input.GetKey(code);
        }
        
    }
}