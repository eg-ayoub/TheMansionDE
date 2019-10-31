using UnityEngine;
using Environment.DoorSystem;
namespace Environment.Collectibles
{
    public class Collectible : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GetComponentInParent<Door>().AddCollectible(this);
            }
        }
    }
}