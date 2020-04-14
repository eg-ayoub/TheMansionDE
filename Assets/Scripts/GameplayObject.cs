using UnityEngine;
public abstract class GameplayObject : MonoBehaviour
{
    protected bool paused = true;

    public virtual void OnPauseGame()
    {
        Animator animator = GetComponent<Animator>();
        if (animator)
        {
            animator.speed = 0f;
        }
        paused = true;
    }

    public virtual void OnResumeGame()
    {
        Animator animator = GetComponent<Animator>();
        if (animator)
        {
            animator.speed = 1;
        }
        paused = false;
    }
}