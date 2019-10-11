using UnityEngine;
namespace InputManagement
{
    public class AxisButtonMap : ButtonMap{
        string axisName;
        float targetValue;

        const float eps = .01f;

        bool pastState;
        bool state;

        public AxisButtonMap(string axisName, float targetValue){
            this.axisName = axisName;
            this.targetValue = Mathf.Clamp(targetValue, -1, 1);

        }

        public void Update(){
            pastState = state;
            state = Mathf.Abs(targetValue - Input.GetAxis(axisName)) <= eps;
        }


        protected override bool Get()
        {
            return state;
        }

        protected override bool GetDown()
        {
            return !pastState && state;
        }

        protected override bool GetUp()
        {
            return pastState && !state;
        }
    }
}