using UnityEngine;
namespace Environment.Platforms
{
    public class GoThroughPlatform : Platform
    {

        GameObject platform;
        private void Start()
        {
            platform = transform.GetChild(0).gameObject;
        }
        public override void PlayerOn()
        {
            platform.SetActive(false);
        }
        public override void PlayerOff()
        {
            platform.SetActive(true);
        }
    }
}