using UnityEngine;
using Environment.DoorSystem;
namespace Environment.Collectibles
{
    public class Collectible : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                FindObjectOfType<Door>().AddCollectible();
                Destroy(gameObject);
            }
        }
    }
}