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
            ParticleSystem runParticles;
            /// <summary>
            /// animator component that switches between animations
            /// </summary>
            Animator animator;
            private void Start()
            {
                animator = GetComponent<Animator>();
                runParticles = transform.GetChild(0).GetComponent<ParticleSystem>();
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
                    SetRunningVolume(Mathf.Abs(blend));
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
                    runParticles.Pause();
                    runParticles.Clear();
                    PauseRunningSoundEffect();
                    ResetTriggers();
                    animator.SetFloat("nodoublejump", 0);
                    animator.SetTrigger("jump");
                }
            }

            public void DoubleJump()
            {
                if (!paused)
                {
                    runParticles.Pause();
                    runParticles.Clear();
                    PauseRunningSoundEffect();
                    ResetTriggers();
                    animator.SetFloat("nodoublejump", 1);
                    animator.SetTrigger("jump");
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
                    animator.SetFloat("nodoublejump", 0);
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
                    PauseRunningSoundEffect();
                    runParticles.Pause();
                    runParticles.Clear();
                    ResetTriggers();
                    animator.SetTrigger("fall");
                }
            }

            public void Wake()
            {
                if (!paused)
                {
                    ResetTriggers();
                    animator.SetTrigger("wake");
                }
            }

            public void _Wake()
            {
                PlayerInstanciationScript.player._WakeUp();
            }

            public void Sleep()
            {
                if (!paused)
                {
                    ResetTriggers();
                    animator.SetTrigger("sleep");
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

            public void CreateDustOnLand()
            {
                GameObject effect = Instantiate(Resources.Load("Effects/Player/GroundHitParticles")) as GameObject;
                effect.transform.position = transform.position - 100 * Vector3.up;
                Destroy(effect, 2f);
                runParticles.Play();
                PlayLandingSoundEffect();
                PlayRunningSoundEffect();
                SetRunningVolume(0);
            }


            public void PlayRunningSoundEffect()
            {
                PlayerInstanciationScript.playerAudio.PlayRunnigSoundEffect();
            }
            public void PauseRunningSoundEffect()
            {
                PlayerInstanciationScript.playerAudio.PauseRunningSoundEffect();
            }
            public void SetRunningVolume(float volume)
            {
                PlayerInstanciationScript.playerAudio.SetRunningVolume(volume);
            }

            public void PlayLandingSoundEffect()
            {
                PlayerInstanciationScript.playerAudio.PlayLandingSoundEffect();
            }

        }
    }
}
