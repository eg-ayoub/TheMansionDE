using UnityEngine;
using Management;
namespace Environment.DoorSystem
{
    public class Door : GameplayObject
    {
        private Key key;
        private bool unlocked;

        private void Start()
        {
            key = GetComponentInChildren<Key>();
            key.Init(this);
        }

        public void Unlock()
        {
            unlocked = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (unlocked && other.CompareTag("Player") && !paused)
            {
                GameManagerScript.gameManager.Next();
            }
        }
    }
}