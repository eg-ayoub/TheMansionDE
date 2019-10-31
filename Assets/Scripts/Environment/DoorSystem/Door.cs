using UnityEngine;
using Management;
using Environment.Collectibles;
namespace Environment.DoorSystem
{
    public class Door : GameplayObject
    {

        public Sprite openSprite;
        public Sprite closedSprite;
        private Key key;
        private bool unlocked;
        bool hasCollectible;

        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = closedSprite;
            key = GetComponentInChildren<Key>();
            key.Init(this);
        }

        public void Unlock()
        {
            GetComponent<SpriteRenderer>().sprite = openSprite;
            unlocked = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (unlocked && other.CompareTag("Player") && !paused)
            {
                unlocked = false;
                if (hasCollectible)
                {
                    GameManagerScript.gameManager.IncrementCollectibles();
                }
                GameManagerScript.gameManager.Next();
            }
        }

        public void AddCollectible()
        {
            this.hasCollectible = true;
        }
    }
}