using UnityEngine;
using PhysicsTools;
using Player;

namespace Environment.Enemies
{
    public class CrawlerBehaviour : GameplayObject
    {
        Animator animator;
        public enum CDIRECTION
        {
            LEFT = -1,
            RIGHT = 1
        };
        public CDIRECTION direction;
        public LayerMask layerMask;
        SensorOnOff groundSensor;
        Vector2 deltaPosition;
        Vector2 speed;
        BoxCollider2D box;
        Rectangle rectangle;

        const float skinWidth = .015f;
        const float gravity = 14800;
        const float crawlSpeed = 500f;
        const int rayCount = 3;
        float skinX;
        float skinY;

        private void Start()
        {
            animator = GetComponent<Animator>();
            groundSensor = GetComponentInChildren<SensorOnOff>();
            box = GetComponent<BoxCollider2D>();
            rectangle = new Rectangle(box, 0);
            skinX = rectangle.B.x;
            skinY = rectangle.B.y;
            rectangle.Reset(box, skinWidth);
            skinX -= rectangle.B.x;
            skinY -= rectangle.B.y;
            transform.GetChild(1).localScale = new Vector3((int)direction, 1, 1);
        }

        private void Update()
        {
            if (!paused)
            {
                animator.ResetTrigger("Fall");
                animator.ResetTrigger("Crawl");
                rectangle.Reset(box, skinWidth);
                if (!groundSensor.GetState())
                {
                    animator.SetTrigger("Fall");
                    speed += gravity * Time.deltaTime * Vector2.down;
                    speed.x = 0;
                }
                else
                {
                    animator.SetTrigger("Crawl");
                    speed.y = 0;
                    speed.x = ((int)direction) * crawlSpeed;
                }
                deltaPosition = speed * Time.deltaTime;
                HorizontalMove();
                VerticalMove();
                transform.Translate(deltaPosition);
            }
        }

        private void VerticalMove()
        {
            int directionY = (int)Mathf.Sign(deltaPosition.y);
            float RayCastLenght = Mathf.Abs(deltaPosition.y);
            Vector2 RayCastOrigin = (directionY > 0 ? rectangle.A : rectangle.D);
            float raySpacing = Mathf.Abs((rectangle.B - rectangle.A).x) / (rayCount - 1);
            for (int i = 0; i < rayCount; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(RayCastOrigin + raySpacing * i * Vector2.right, directionY * Vector2.up, RayCastLenght, layerMask);
                if (hit)
                {
                    deltaPosition.y = directionY * (hit.distance - skinY);
                    RayCastLenght = hit.distance;
                }
            }
        }

        private void HorizontalMove()
        {
            int directionX = (int)Mathf.Sign(deltaPosition.x);
            float RayCastLenght = Mathf.Abs(deltaPosition.x);
            Vector2 RayCastOrigin = (directionX > 0 ? rectangle.C : rectangle.D);
            float raySpacing = Mathf.Abs((rectangle.B - rectangle.C).y) / (rayCount - 1);
            for (int i = 0; i < rayCount; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(RayCastOrigin + raySpacing * i * Vector2.up, directionX * Vector2.right, RayCastLenght, layerMask);
                if (hit)
                {
                    deltaPosition.x = directionX * (hit.distance - skinX);
                    RayCastLenght = hit.distance;
                }
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