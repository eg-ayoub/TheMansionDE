using UnityEngine;
using Player.Health;
using Player;

namespace Environment.Hazards
{
    public class DeadZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerInstanciationScript.hpManager.TakeDamage();
            }
        }
    }
}