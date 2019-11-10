using UnityEngine;

namespace UI.HUD
{
    public class KeyStatusDisplay : MonoBehaviour
    {
        ImageIndicator key;
        private void Start()
        {
            key = GetComponentInChildren<ImageIndicator>();
        }
        public void UpdateKeyStatus(bool status)
        {
            if (status) key.On(); else key.Off();
        }
    }
}