using UnityEngine;
namespace Player
{
    namespace Movement
    {
        /// <summary>
        /// monobehaviour that manages the ground vector of the surface underneath the player
        /// </summary>
        public class GroundVectorCalculator : GameplayObject
        {
            /// <summary>
            /// estimated angle of the ground normal underneath the player
            /// </summary>
            float angle1, angle2;
            /// <summary>
            /// assumed angle of the ground normal underneath the player 
            /// </summary>
            float angle;
            /// <summary>
            /// depth to which we seek for a ground underneath the player
            /// </summary>
            int maxRaycastDepth;
            /// <summary>
            /// layers to look for ground in
            /// </summary>
            [SerializeField, Header("Ground Layer")]
            LayerMask layerMask;
            /// <summary>
            /// collision sensor script to store ground vector
            /// </summary>
            CollisionSensorScript sensor;

            // Use this for initialization
            void Start()
            {
                sensor = GetComponentInParent<CollisionSensorScript>();
                paused = false;
            }

            // Update is called once per frame
            void FixedUpdate()
            {
                if (!paused)
                {
                    angle1 = angle2 = 0;
                    Cast(ref angle1, true);
                    Cast(ref angle2, false);
                    angle = (Mathf.Abs(angle1) > Mathf.Abs(angle2) ? angle1 : angle2);
                    sensor.info.groundVector = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));

                }
            }
            /// <summary>
            /// casts a ray from location pointed by dir (true for botttomleft, false for bottomright)
            /// writes result in angle.
            /// </summary>
            /// <param name="angle">variable where we store ground vector angle</param>
            /// <param name="dir"> location from which we cast ray</param>
            void Cast(ref float angle, bool dir)
            {
                RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + new Vector2((dir ? -1 : 1) * 6, 5), -Vector2.up, 100, layerMask);
                if (hit)
                {
                    angle = Vector2.SignedAngle(Vector2.up, hit.normal);
                }
            }
        }
    }
}

