using UnityEngine;
using InputManagement;
using Management;

namespace Environment.HubWorld
{
    public class HubDoor : GameplayObject
    {
        public Sprite accessibleSprite;
        public Sprite inaccessibleSprite;
        public Sprite finishedSprite;
        SpriteRenderer sprite;

        public bool accessible;

        public bool finished;

        public int time;

        public int targetLevel;

        private void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        public void Init()
        {
            if (accessible)
            {
                sprite.sprite = accessibleSprite;
                if (finished)
                {
                    sprite.sprite = finishedSprite;
                }
            }
            else
            {
                sprite.sprite = inaccessibleSprite;
            }

        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !paused && accessible)
            {
                if (KeyMapper.GetButtonDown("Start"))
                {
                    GameManagerScript.gameManager.Enter(targetLevel);
                }
            }
        }
    }
}