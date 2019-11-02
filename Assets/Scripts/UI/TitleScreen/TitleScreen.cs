using UnityEngine;
using Management;
using InputManagement;

namespace UI.TitleScreen
{
    public class TitleScreen : MonoBehaviour
    {
        public static TitleScreen titleScreen;
        Animator animator;
        bool finished;
        bool fadedIn;

        private void Awake()
        {
            if (titleScreen == null)
            {
                titleScreen = this;
                DontDestroyOnLoad(this);
            }
            else if (titleScreen != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            animator.SetTrigger("Start");
            GameManagerScript.gameManager.LockPause();
        }

        private void Update()
        {
            if (!finished && KeyMapper.AnyKeyDown() && fadedIn)
            {
                LaunchGame();
            }
        }

        private void LaunchGame()
        {
            finished = true;
            GameManagerScript.gameManager.UnLockPause();
            GameManagerScript.gameManager.ToggleGamePaused();
            animator.SetTrigger("Launch");

        }

        public void FadeInFinished()
        {
            fadedIn = true;
        }
    }
}