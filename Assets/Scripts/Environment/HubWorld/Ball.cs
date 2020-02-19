using UnityEngine;
namespace Environment.HubWorld
{
    public class Ball : GameplayObject
    {

        // * when you get bored of it all,
        // * as you have a game in the making,
        // * just drop it all and play ball...
        // * it's not as hard as working

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