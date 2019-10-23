using System.Collections;
using UnityEngine;
namespace Player
{
    using Graphics;
    using Controls;
    namespace Movement
    {
        /// <summary>
        /// this script defines the player movement mechanics
        /// </summary>
        public class PlayerMovementModifier : GameplayObject
        {

            /// <summary>
            /// normal vector of the ground under the player
            /// </summary>
            Vector2 groundVector;
            /// <summary>
            /// player's speed vector
            /// </summary>
            Vector2 Speed;
            /// <summary>
            /// player's physics manager 
            /// </summary>
            PlayerPhysics playerPhysics;
            /// <summary>
            /// player's sensors for wall, ground and ceiling collisions
            /// </summary>
            CollisionSensorScript sensor;
            /// <summary>
            /// manages animation and sorts
            /// </summary>
            PlayerGraphicsManager graphicsManager;

            /// <summary>
            /// player environment collision state
            /// </summary>
            bool playerOnRightWall, playerOnLeftWall, playerOnGround, playerOnCeiling, playerWasOnGround;
            /// <summary>
            /// in the time-in phase of leaving a platform
            /// </summary>
            bool jumpTimingIn;
            /// <summary>
            /// jump control
            /// </summary>
            [HideInInspector] public bool jumpButtonDown, jumpButtonUp;
            /// <summary>
            /// joystick 
            /// </summary>
            float horizontal;
            /// <summary>
            /// double jump state : false if the player has just used doublejump
            /// </summary>
            bool canDoubleJump;
            /// <summary>
            /// max horizontal speed when player is running
            /// </summary>
            [SerializeField, Header("Running constants")] float maxRunSpeed;
            /// <summary>
            /// max horizontal speed when player is airborne
            /// </summary>
            float maxAirborneSpeed;
            /// <summary>
            /// max vertical speed the player can reach while jumping
            /// </summary>
            [HideInInspector] public float jumpSpeedY0;
            /// <summary>
            /// max vertical speed the player can reach during second jump
            /// </summary>
            [SerializeField] float doubleJumpSpeedY0;
            /// <summary>
            /// max height the player can reach while jumping
            /// </summary>
            [SerializeField, Header("Jump constants")] float jumpHeight;
            /// <summary>
            /// max height player can reach when jumping from a wall
            /// </summary>
            [SerializeField] float jumpHeightFromWall;
            /// <summary>
            /// max height player can reach during the second jump 
            /// </summary>
            [SerializeField] float doubleJumpHeight;
            /// <summary>
            /// max time player can spend airborne before reaching the same y coordinate
            /// </summary>
            [SerializeField] float jumpTime;
            /// <summary>
            /// max x distance the player can cover while jumping before reaching the same y coordinate
            /// </summary>
            [SerializeField] float maxJumpWidth;
            /// <summary>
            /// max vertical speed the player reaches when jumping from wall
            /// </summary>
            float climbSpeed;
            /// <summary>
            /// friction force coefficient 
            /// </summary>
            [Range(0, 1), SerializeField, Header("Slope constants")] float slideParameter;
            /// <summary>
            /// min slope angle the player starts sliding after
            /// </summary>
            [Range(0, 1), SerializeField] float minSlopeSlideX;
            /// <summary>
            /// speed the player slides with following the slope vecor
            /// </summary>
            [SerializeField] float slopeSlideSpeed;
            /// <summary>
            /// indicates movement state of the player
            /// </summary>
            public bool moving;
            bool trampolineTrigger;
            bool breakJump;

            void Start()
            {
                sensor = GetComponentInChildren<CollisionSensorScript>();
                playerPhysics = GetComponent<PlayerPhysics>();
                graphicsManager = PlayerInstanciationScript.graphicsManager;
                groundVector = Vector2.right;
                playerPhysics.SetGravity((8 * jumpHeight) / (jumpTime * jumpTime));
                maxAirborneSpeed = maxJumpWidth / jumpTime;
                jumpSpeedY0 = (4 * jumpHeight) / jumpTime;
                climbSpeed = Mathf.Sqrt(2 * playerPhysics.GetGravity() * jumpHeightFromWall);
                doubleJumpSpeedY0 = Mathf.Sqrt(2 * playerPhysics.GetGravity() * doubleJumpHeight);

                paused = false;

            }
            /// <summary>
            /// setter for player's speed
            /// </summary>
            /// <param name="TS">new speed</param>
            public void SetSpeed(Vector2 TS)
            {
                Speed = TS;
            }


            /// <summary>
            /// gets controls from playercontroller
            /// </summary>
            /// <param name="JUP">jump button up</param>
            /// <param name="JDOWN">jump button up</param>
            /// <param name="h">horizontal stick axis</param>
            /// <param name="v">vertical stick axis</param>
            public void SetControls(bool JUP, bool JDOWN, float h)
            {
                jumpButtonUp = JUP;
                jumpButtonDown = JDOWN;
                horizontal = h;
            }

            private void Update()
            {
                if (!paused)
                {
                    playerOnGround = sensor.info.down;
                    playerOnCeiling = sensor.info.up;
                    playerOnRightWall = sensor.info.right;
                    playerOnLeftWall = sensor.info.left;
                    groundVector = sensor.info.groundVector;

                    if (!(playerOnRightWall || playerOnLeftWall) || playerOnGround)
                    {
                        graphicsManager.SetRunBlend(horizontal);
                    }

                    if (playerOnGround)
                    {
                        //Debug.Log(Speed.y);
                        graphicsManager.Land();
                        canDoubleJump = true;
                    }

                    if (Mathf.Abs(horizontal) <= 10E-2)
                    {
                        if (playerOnGround)
                        {
                            Speed.x *= slideParameter;
                            Speed.y = 0;
                            if (sensor.info.groundVector.x < minSlopeSlideX)
                            {
                                Speed = -slopeSlideSpeed
                                        * Mathf.Sign(sensor.info.groundVector.y)
                                        * groundVector;
                            }
                            canDoubleJump = true;
                        }
                        else
                        {
                            Speed.x *= .05f;
                        }

                    }
                    else if (!playerOnLeftWall && !playerOnRightWall)
                    {
                        if (playerOnGround)
                        {
                            Speed = horizontal * maxRunSpeed * groundVector;
                        }
                        else
                        {
                            Speed.x = horizontal * maxAirborneSpeed;
                        }

                    }
                    else if (playerOnGround)
                    {
                        canDoubleJump = true;
                        Speed = horizontal * maxRunSpeed * groundVector;
                    }
                    else
                    {
                        Speed.y *= .5f;

                    }

                    if (jumpButtonDown)
                    {
                        if (playerOnGround || jumpTimingIn)
                        {
                            if (jumpTimingIn)
                            {
                                Debug.Log("Time-in jump!");
                            }
                            graphicsManager.Jump();
                            Speed.y = jumpSpeedY0;
                        }
                        else if (playerOnLeftWall || playerOnRightWall)
                        {
                            // ? what do I do about jump on wall
                            // if (jumpOnWallFuse)
                            // {
                            //     Speed.y = climbSpeed;
                            //     Speed.x = (playerOnLeftWall ? 1 : -1) * maxAirborneSpeed;
                            //     graphicsManager.ClimbJump();
                            //     //TODO:speed.x depends on the control
                            // }
                            // else
                            // {
                            //     Speed.x = (playerOnLeftWall ? 1 : -1) * maxAirborneSpeed;
                            // }
                        }
                        else if (canDoubleJump)
                        {
                            graphicsManager.DoubleJump();
                            Speed.y = doubleJumpSpeedY0;
                            canDoubleJump = false;
                        }

                    }

                    if (jumpButtonUp && Speed.y > 0 && !playerOnGround)
                    {
                        Speed.y = 0;
                    }

                    if (breakJump && Speed.y > 0 && !playerOnGround)
                    {
                        Speed.y = 0;
                        breakJump = false;
                    }

                    if (!playerOnGround && Speed.y < -100)
                    {
                        graphicsManager.Fall();
                    }


                    if (playerOnRightWall || playerOnLeftWall)
                    {
                        graphicsManager.TouchWall();
                    }

                    if (trampolineTrigger)
                    {
                        graphicsManager.Jump();
                        Speed.y = jumpSpeedY0;
                        jumpSpeedY0 = (4 * jumpHeight) / jumpTime;
                        trampolineTrigger = false;
                    }

                    if (playerWasOnGround && !playerOnGround && Speed.y <= 100)
                    {
                        StartCoroutine(JumpTimeInOut());
                    }

                    playerPhysics.SetTargetSpeed(Speed);
                    playerWasOnGround = playerOnGround;
                }
            }

            IEnumerator JumpTimeInOut()
            {
                jumpTimingIn = true;
                for (int i = 0; i < 5; i++)
                {
                    yield return null;
                }
                jumpTimingIn = false;
            }

            /// <summary>
            /// triggers a jump that reaches y + pinnacle
            /// </summary>
            /// <param name="pinnacle">the altitude to reach</param>
            public void TrampolineJump(float pinnacle)
            {
                jumpSpeedY0 = Mathf.Sqrt(2 * pinnacle * playerPhysics.GetGravity());
                trampolineTrigger = true;
            }

            public void BreakJump()
            {
                if (!playerOnGround && Speed.y > 0)
                {
                    breakJump = true;
                }
            }

        }
    }
}
