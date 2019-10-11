using UnityEngine;
public abstract class GameplayObject : MonoBehaviour
{
    protected bool paused;

    public virtual void OnPauseGame()
    {
        paused = true;
    }

    public virtual void OnResumeGame()
    {
        paused = false;
    }
}