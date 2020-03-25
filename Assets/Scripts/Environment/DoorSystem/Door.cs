using UnityEngine;
using Management;
using Environment.Collectibles;
using System.Collections;
namespace Environment.DoorSystem
{
    public class Door : GameplayObject
    {
        private Key key;
        private bool unlocked;
        bool hasCollectible;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                GameManagerScript.gameManager.Next();
            }
        }

        private void Start()
        {
            key = GetComponentInChildren<Key>();
            key.Init(this);
        }

        public void Unlock()
        {
            unlocked = true;
            StartCoroutine(AlphaUnlock());
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

        IEnumerator AlphaUnlock()
        {
            GetComponent<AudioSource>().Play();
            yield return null;
            for (int i = 0; i < 15; i++)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (float)(14 - i) / 14);
                yield return null;
            }
        }
    }
}