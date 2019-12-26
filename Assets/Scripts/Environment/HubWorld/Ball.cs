using UnityEngine;
namespace Environment.HubWorld
{
    public class Ball : GameplayObject
    {

        public float kickForce;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Vector2 contact = other.contacts[0].point;
                contact = (Vector2)transform.position - contact;
                GetComponent<Rigidbody2D>().AddForce(kickForce * Vector2.up + 1000 * contact.normalized);
            }
        }
    }
}