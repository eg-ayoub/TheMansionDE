using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Loading
{
    public class LoadingOverlay : MonoBehaviour
    {

        AudioSource audioSource;

        public enum ANIMATIONS
        {
            NONE = 0,
            RESTART,
            DEATH,
            NEXT_LEVEL,
            HUB_ON_SUCCESS,
            LEVEL_ENTRY,
            GAME_OVER

        };
        public static LoadingOverlay overlay;

        public bool isIdle;
        public bool idlingDone;
        public bool isDone;
        ANIMATIONS clip;
        Animator animator;
        Text overlayMessage;
        Image overlayMessageDesc;
        public Sprite deathSprite;
        public Sprite winSprite;

        private void Awake()
        {
            if (overlay == null)
            {
                overlay = this;
                DontDestroyOnLoad(this);
            }
            else if (overlay != this)
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            audioSource = GetComponentInChildren<AudioSource>();
            animator = GetComponent<Animator>();
            overlayMessage = GetComponentInChildren<Text>();
            overlayMessageDesc = transform.GetChild(6).GetComponent<Image>();
            RemoveIndicators();
        }
        public void Play(ANIMATIONS animation)
        {
            switch (animation)
            {
                case ANIMATIONS.RESTART:
                case ANIMATIONS.DEATH:
                    clip = animation;
                    animator.SetTrigger("Restart");
                    break;
                case ANIMATIONS.HUB_ON_SUCCESS:
                case ANIMATIONS.LEVEL_ENTRY:
                case ANIMATIONS.NEXT_LEVEL:
                    clip = animation;
                    animator.SetTrigger("Next");
                    break;
                case ANIMATIONS.GAME_OVER:
                    clip = animation;
                    animator.SetTrigger("GameOver");
                    break;
                default:
                    break;
            }
        }

        public void Resume()
        {
            animator.SetTrigger("Resume");
        }
        public void Reset()
        {
            isIdle = isDone = idlingDone = false;
            clip = ANIMATIONS.NONE;
        }
        public void Idling()
        {
            isIdle = true;
        }

        public void IdlingDone()
        {
            idlingDone = true;
        }
        public void Done()
        {
            isDone = true;
        }

        internal void DisplayWin()
        {
            audioSource.Play();
            overlayMessageDesc.sprite = winSprite;
            overlayMessageDesc.color = Color.white;
        }

        internal void DisplayGameOver()
        {
            overlayMessage.text = "GAME OVER";
            overlayMessage.color = Color.white;

            overlayMessageDesc.sprite = deathSprite;
            overlayMessageDesc.color = Color.white;
        }

        internal void RemoveIndicators()
        {
            overlayMessage.color = Color.clear;
            overlayMessageDesc.color = Color.clear;
        }

        // internal void DisplayLevel()
    }
}