using UnityEngine;
namespace InputManagement
{
    public class AxisMap{
        string axisName;

        public AxisMap(string axisName){
            this.axisName = axisName;
        }

        public float GetAxis(){
            return Input.GetAxis(axisName);
        }

        public float GetAxisRaw(){
            return Input.GetAxisRaw(axisName);
        }
    }
}