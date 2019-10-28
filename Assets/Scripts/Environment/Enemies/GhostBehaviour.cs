using UnityEngine;
using Player;
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
        const float approachDistance = 500f;
        const float maxDistance = 2000f;
        float tau = .6f;

        private void Start()
        {
            CalculateParams();
        }

        private void CalculateParams()
        {
            switch (direction)
            {
                case GDIRECTION.CENTER:
                    offset = approachDistance * Vector3.zero;
                    break;
                case GDIRECTION.UP:
                    offset = approachDistance * Vector3.up;
                    break;
                case GDIRECTION.DOWN:
                    offset = approachDistance * Vector3.down;
                    break;
                case GDIRECTION.LEFT:
                    offset = approachDistance * Vector3.left;
                    break;
                case GDIRECTION.RIGHT:
                    offset = approachDistance * Vector3.right;
                    break;
            }

            float distance = Mathf.Clamp(
                (transform.position
                    - PlayerInstanciationScript.playerTransform.position).magnitude,
                approachDistance,
                maxDistance
            );

            tau = .1f + .5f * (
                (distance - approachDistance)
                / (maxDistance - approachDistance)
            );
        }

        private void Update()
        {
            if (!paused)
            {
                Vector3 target =
                    PlayerInstanciationScript
                        .playerTransform
                        .transform
                        .position
                    + offset;

                Vector3 deltaPosition = 1 / tau * (target - transform.position) * Time.deltaTime;
                deltaPosition.z = 0;

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

    }
}