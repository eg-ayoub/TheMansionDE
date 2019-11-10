using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class LevelDisplay : MonoBehaviour
    {
        Text text;
        private void Start()
        {
            text = GetComponent<Text>();
        }
        public void UpdateLevel(int level)
        {
            text.text = "" + level;
        }
    }
}