using UnityEngine;

namespace UI.Loading
{
    public class LoadingOverlay : MonoBehaviour
    {
        public enum ANIMATIONS
        {
            NONE = 0,
            RESTART,
            GAME_OVER,
            NEXT_LEVEL,
            HUB_ON_SUCCESS,
            LEVEL_ENTRY

        };
        public static LoadingOverlay overlay;

        public bool isIdle;
        public bool isDone;
        ANIMATIONS clip;
        Animator animator;

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
            animator = GetComponent<Animator>();
        }
        public void Play(ANIMATIONS animation)
        {
            switch (animation)
            {
                case ANIMATIONS.RESTART:
                    clip = animation;
                    animator.SetTrigger("Restart");
                    break;
                case ANIMATIONS.GAME_OVER:
                    clip = animation;
                    animator.SetTrigger("Checkpoint");
                    break;
                case ANIMATIONS.LEVEL_ENTRY:
                case ANIMATIONS.NEXT_LEVEL:
                    clip = animation;
                    animator.SetTrigger("Next");
                    break;
                default:
                    break;
            }
        }

        public void Resume()
        {
            if (clip > 0)
            {
                animator.SetTrigger("Resume");
            }
        }
        public void Reset()
        {
            if (clip > 0)
            {
                isIdle = isDone = false;
                clip = ANIMATIONS.NONE;
            }
        }
        public void Idling()
        {
            if (clip > 0)
            {
                isIdle = true;
            }
        }
        public void Done()
        {
            if (clip > 0)
            {
                isDone = true;
            }
        }

    }
}