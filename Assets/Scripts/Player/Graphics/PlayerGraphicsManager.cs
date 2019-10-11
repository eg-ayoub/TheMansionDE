using System;
using System.Collections;
using UnityEngine;
namespace Player
{
    namespace Graphics
    {
        /// <summary>
        /// manages the player's animation and look 
        /// </summary>
        public class PlayerGraphicsManager : GameplayObject
        {
            /// <summary>
            /// animator component that switches between animations
            /// </summary>
            Animator animator;
            private void Start()
            {
                animator = GetComponent<Animator>();
                StartCoroutine(BlinkCycle());
            }
            /// <summary>
            /// sets the runblend for interpolation between run and idle anim
            /// </summary>
            /// <param name="blend">new blend value</param>
            public void SetRunBlend(float blend)
            {
                if (!paused)
                {
                    animator.SetFloat("runblend", Mathf.Abs(blend));
                    if (blend != 0)
                    {
                        SetDirectionX(blend > 0);
                    }
                }
            }
            /// <summary>
            /// sets the direction player avatar is looking at 
            /// </summary>
            /// <param name="right">wether the avatar is facing to the right side of the screen</param>
            void SetDirectionX(bool right)
            {
                if (!paused)
                {
                    if ((right ? -1 : 1) * Mathf.Sign(transform.localScale.x) == -1)
                    {
                        Flip();
                    }
                    float newScaleX = (right ? -1 : 1) * Mathf.Abs(transform.localScale.x);
                    StartCoroutine(FlipCoroutine(newScaleX));
                }
            }
            IEnumerator FlipCoroutine(float newScaleX)
            {
                for (int _ = 0; _ < 5; _++)
                {
                    if (paused)
                    {
                        _--;
                    }
                    yield return null;
                }
                transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
            }
            /// <summary>
            /// sets the direction player avatar is looking at 
            /// </summary>
            /// <param name="right">wether the avatar is facing to the right side of the screen</param>
            void SetDirectionY(bool up)
            {
                if (!paused)
                {
                    Vector3 newScale = transform.localScale;
                    newScale.y = (up ? -1 : 1) * Mathf.Abs(newScale.y);
                    transform.localScale = newScale;
                }
            }


            public void Flip()
            {
                if (!paused)
                {
                    ResetTriggers();
                    animator.SetTrigger("flip");
                }
            }
            /// <summary>
            /// plays jump anim
            /// </summary>
            public void Jump()
            {
                if (!paused)
                {
                    ResetTriggers();
                    animator.SetTrigger("jump");
                }
            }

            public void DoubleJump()
            {
                if (!paused)
                {
                    Jump();
                    // ! replace me
                }
            }
            /// <summary>
            /// go back to run anim
            /// </summary>
            public void Land()
            {
                if (!paused)
                {
                    ResetTriggers();
                    animator.SetTrigger("land");
                }
            }
            /// <summary>
            /// free fall anim
            /// </summary>
            public void Fall()
            {
                if (!paused)
                {
                    ResetTriggers();
                    animator.SetTrigger("fall");
                }
            }

            public void TouchWall()
            {
                if (!paused)
                {
                    ResetTriggers();
                    animator.SetTrigger("wall");
                }
            }

            public void ResetTriggers()
            {
                animator.ResetTrigger("jump");
                animator.ResetTrigger("land");
                animator.ResetTrigger("fall");
                animator.ResetTrigger("wall");
                //animator.ResetTrigger("flip");
            }

            IEnumerator BlinkCycle()
            {
                yield return null;
                bool hasBlinked = false;
                while (true)
                {
                    animator.SetFloat("blinkblend", 1);
                    for (int _ = 0; _ < 8; _++)
                    {
                        if (paused)
                        {
                            _--;
                        }
                        yield return null;
                    }
                    animator.SetFloat("blinkblend", 0);
                    yield return null;
                    yield return null;
                    int waitTime = UnityEngine.Random.Range(hasBlinked ? 15 : 0, 60);
                    if (hasBlinked)
                    {
                        hasBlinked = false;
                    }
                    else
                    {
                        if (waitTime == 0)
                        {
                            hasBlinked = true;
                        }
                    }
                    for (int _ = 0; _ < waitTime * 4; _++)
                    {
                        if (paused)
                        {
                            _--;
                        }
                        yield return null;
                    }
                }
            }

            public override void OnPauseGame()
            {
                animator.speed = 0;
                base.OnPauseGame();
            }

            public override void OnResumeGame()
            {
                animator.speed = 1;
                base.OnResumeGame();
            }
        }
    }
}
