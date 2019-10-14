using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    [RequireComponent(typeof(Image))]
    public class ImageIndicator : MonoBehaviour
    {
        Image image;
        private void Start()
        {
            image = GetComponent<Image>();
        }
        public void On()
        {
            image.color = Color.white;
        }
        public void Off()
        {
            image.color = new Color(0, 0, 0, 0);
        }
    }
}