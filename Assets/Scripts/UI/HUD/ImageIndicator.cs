using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    [RequireComponent(typeof(Image))]
    public class ImageIndicator : MonoBehaviour
    {
        public Sprite on;
        public Sprite off;
        public Sprite mansion;

        public Sprite broken;
        Image image;
        private void Start()
        {
            image = GetComponent<Image>();
            image.sprite = on;
            Off();
        }
        public void On()
        {
            image.color = Color.white;
            image.sprite = on;
        }
        public void Off()
        {
            if (off == null)
            {
                image.color = new Color(.1f, .1f, .1f, 1);
            }
            else
            {
                image.sprite = off;
            }
        }

        public void Mansion()
        {
            if (mansion == null)
            {
                image.color = new Color(.1f, .1f, .1f, 0);
            }
            else
            {
                image.sprite = mansion;
            }
        }

        public void Broken()
        {
            if (mansion == null)
            {
                image.color = new Color(.1f, .1f, .1f, 0);
            }
            else
            {
                image.sprite = broken;
            }
        }
    }
}