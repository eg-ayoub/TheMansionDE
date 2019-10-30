using UnityEngine;
using PhysicsTools;
namespace Player
{
    namespace Movement
    {
        /// <summary>
        /// the clip manager applies player movement through delta position translation.
        /// this script calculates collisions and rectifies the movement tot avoid clipping through the environment elements.
        /// 
        /// DO NOT EDIT THIS SCRIPT
        /// </summary>
        public class PlayerClipManager : GameplayObject
        {
            PlayerPhysics playerPhysics;
            /// <summary>
            /// translation value for this scene
            /// </summary>
            Vector2 deltaPosition;
            /// <summary>
            /// player hitbox
            /// </summary>
            BoxCollider2D PlayerBox;
            /// <summary>
            /// player hitbox cast to Rectangle object
            /// </summary>
            Rectangle PlayerRect;
            /// <summary>
            /// width of the collider's skin
            /// </summary>
            const float skinWidth = .015f;
            /// <summary>
            /// <see cref='skinWidth'/> /x
            /// </summary>
            float XW;
            /// <summary>
            /// <see cref='skinWidth'/> /y
            /// </summary>
            float YW;
            /// <summary>
            /// number of rays to cast horizontally
            /// </summary>
            [SerializeField, Header("Ray casting constants")]
            int HorizontalRayCount;
            /// <summary>
            /// number of rays to cast vertically
            /// </summary>
            [SerializeField] int VerticalRayCount;
            /// <summary>
            /// layers player collides with
            /// </summary>
            [SerializeField] LayerMask layerMask;
            /// <summary>
            /// space between the rays cast horizontally
            /// </summary>
            float HorizontalRaySpacing;
            /// <summary>
            /// space between the rays cast vertically
            /// </summary>
            float VerticalRaySpacing;
            Transform playerTransform;

            bool frozen;

            void Start()
            {
                PlayerBox = GetComponentInParent<BoxCollider2D>();
                PlayerRect = new Rectangle(PlayerBox, 0);
                XW = PlayerRect.B.x;
                YW = PlayerRect.B.y;
                PlayerRect.Reset(PlayerBox, skinWidth);
                XW -= PlayerRect.B.x;
                YW -= PlayerRect.B.y;
                HorizontalRayCount = Mathf.Clamp(HorizontalRayCount, 2, int.MaxValue);
                VerticalRayCount = Mathf.Clamp(VerticalRayCount, 2, int.MaxValue);
                // paused = false;
                playerTransform = PlayerInstanciationScript.playerTransform;
                playerPhysics = PlayerInstanciationScript.player.GetComponentInChildren<PlayerPhysics>();
            }



            void FixedUpdate()
            {
                if (!paused)
                {
                    PlayerRect.Reset(PlayerBox, skinWidth);
                    VerticalMove(ref deltaPosition);
                    HorizontalMove(ref deltaPosition);
                    if (!frozen)
                    {
                        playerPhysics.SetRealDeltaPosition(deltaPosition);
                        playerTransform.Translate(deltaPosition);
                    }
                }
            }

            /// <summary>
            /// cast rays horizontally and deduce new deltaPositon
            /// </summary>
            /// <param name="deltaPosition"> reference to original deltaPosition we deduce from</param>
            void HorizontalMove(ref Vector2 deltaPosition)
            {
                int directionX = (int)Mathf.Sign(deltaPosition.x);
                float RayCastLenght = Mathf.Abs(deltaPosition.x);
                Vector2 RayCastOrigin = (directionX > 0 ? PlayerRect.C : PlayerRect.D);
                HorizontalRaySpacing = Mathf.Abs((PlayerRect.B - PlayerRect.C).y) / (HorizontalRayCount - 1);
                for (int i = 0; i < HorizontalRayCount; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(RayCastOrigin + HorizontalRaySpacing * i * Vector2.up, directionX * Vector2.right, RayCastLenght, layerMask);
                    if (hit)
                    {
                        deltaPosition.x = directionX * (hit.distance - XW);
                        RayCastLenght = hit.distance;
                    }
                }

            }
            /// <summary>
            /// cast rays vertically and 
            /// </summary>
            /// <param name="deltaPosition">reference to original deltaPosition we deduce from</param>
            void VerticalMove(ref Vector2 deltaPosition)
            {
                int directionY = (int)Mathf.Sign(deltaPosition.y);
                float RayCastLenght = Mathf.Abs(deltaPosition.y);
                Vector2 RayCastOrigin = (directionY > 0 ? PlayerRect.A : PlayerRect.D);
                VerticalRaySpacing = Mathf.Abs((PlayerRect.B - PlayerRect.A).x) / (VerticalRayCount - 1);
                for (int i = 0; i < VerticalRayCount; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(RayCastOrigin + VerticalRaySpacing * i * Vector2.right, directionY * Vector2.up, RayCastLenght, layerMask);
                    if (hit)
                    {
                        deltaPosition.y = directionY * (hit.distance - YW);
                        RayCastLenght = hit.distance;
                    }
                }

            }

            /// <summary>
            /// setter for deltaPosition in <see cref='PlayerPhysicsScript'/>
            /// </summary>
            /// <param name="move"> new deltaPosition</param>
            public void SetDeltaPosition(Vector2 move)
            {
                deltaPosition = move;
            }

            public void UnFreeze()
            {
                frozen = false;
            }

            public void Freeze()
            {
                frozen = true;
            }
        }
    }
}
