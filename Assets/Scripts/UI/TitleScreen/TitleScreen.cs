using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UI.TitleScreen
{
    public class TitleScreen : MonoBehaviour
    {
        public static TitleScreen titleScreen;
        Animator animator;

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
        }

        // public void Kill()
        // {
        //     // * un-pause game 
        //     animator.SetTrigger("Kill");
        // }
    }
}