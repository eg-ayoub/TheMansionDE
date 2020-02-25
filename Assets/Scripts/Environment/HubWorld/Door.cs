using UnityEngine;
using InputManagement;

namespace Environment.HubWorld
{
    public abstract class Door : GameplayObject
    {
        public bool accessible;
        public abstract void Enter();

        public void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !paused && accessible)
            {
                if (KeyMapper.GetButtonDown("Start"))
                {
                    Enter();
                }
            }
        }
    }
}