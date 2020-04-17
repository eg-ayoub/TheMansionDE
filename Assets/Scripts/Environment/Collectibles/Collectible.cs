using UnityEngine;
using Environment.DoorSystem;
namespace Environment.Collectibles
{
    public class Collectible : MonoBehaviour
    {

        AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                audioSource.Play();
                FindObjectOfType<Door>().AddCollectible();
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                GetComponentInChildren<CircleCollider2D>().enabled = false;
                Destroy(gameObject, 1f);
            }
        }
    }
}