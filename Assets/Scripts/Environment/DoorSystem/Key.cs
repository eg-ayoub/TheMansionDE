using UnityEngine;
namespace Environment.DoorSystem
{
    public class Key : GameplayObject
    {
        private Door door;

        public void Init(Door door)
        {
            this.door = door;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !paused)
            {
                door.Unlock();
            }
        }
    }
}