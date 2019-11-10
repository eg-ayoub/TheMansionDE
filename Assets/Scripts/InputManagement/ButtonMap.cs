namespace InputManagement
{
    public abstract class ButtonMap{

        public bool ButtonDown(){
            return GetDown();
        }

        public bool ButtonUp(){
            return GetUp();
        }

        public bool Button(){
            return Get();
        }
        protected abstract bool GetDown();

        protected abstract bool GetUp();

        protected abstract bool Get();
    }
}