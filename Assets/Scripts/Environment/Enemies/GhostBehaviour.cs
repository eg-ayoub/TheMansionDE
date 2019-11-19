using UnityEngine;
using Player;
using System.Collections;
namespace Environment.Enemies
{
    public class GhostBehaviour : GameplayObject
    {
        public enum GDIRECTION
        {
            CENTER,
            RIGHT,
            LEFT,
            UP,
            DOWN
        }

        public GDIRECTION direction;
        private Vector3 offset;

        float speedmultiplier;

        public float forgetDistance = 5000f;
        public float speedupDistance = 2000f;
        public float slowdownDistance = 1000f;
        public float attachDistance = 500f;

        public float slowSpeedMultiplier = 1;
        public float fastSpeedMultiplier = 2;

        private void Start()
        {
            CalculateParams();
        }

        private void CalculateParams()
        {
            switch (direction)
            {
                case GDIRECTION.CENTER:
                    offset = attachDistance * Vector3.zero;
                    break;
                case GDIRECTION.UP:
                    offset = attachDistance * Vector3.up;
                    break;
                case GDIRECTION.DOWN:
                    offset = attachDistance * Vector3.down;
                    break;
                case GDIRECTION.LEFT:
                    offset = attachDistance * Vector3.left;
                    break;
                case GDIRECTION.RIGHT:
                    offset = attachDistance * Vector3.right;
                    break;
            }

        }

        private void Update()
        {
            if (!paused)
            {
                Vector3 target = PlayerInstanciationScript
                        .playerTransform
                        .transform
                        .position
                    + offset - transform.position;
                target.z = 0;

                if (target.magnitude > forgetDistance)
                {
                    speedmultiplier = 0;
                }
                else if (target.magnitude > speedupDistance)
                {
                    speedmultiplier = slowSpeedMultiplier;
                }
                else if (target.magnitude > slowdownDistance)
                {
                    speedmultiplier = fastSpeedMultiplier;
                }
                else if (target.magnitude > 10)
                {
                    speedmultiplier = slowSpeedMultiplier;
                }
                else
                {
                    speedmultiplier = 0;
                }

                Vector3 deltaPosition = speedmultiplier * target.normalized * 1000f * Time.deltaTime;

                if (deltaPosition.x > 0) transform.localScale = new Vector3(-1, 1, 1);
                else transform.localScale = new Vector3(1, 1, 1);

                transform.Translate(deltaPosition);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!paused && other.CompareTag("Player"))
            {
                PlayerInstanciationScript.hpManager.TakeDamage();
            }
        }

        IEnumerator SoundCoroutine()
        {
            AudioSource source = GetComponent<AudioSource>();
            while (true)
            {
                source.Play();
                while (!source.isPlaying)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(3f);
            }
        }

    }
}