using UnityEngine;
/// <summary>
/// simple sensor
/// </summary>
public class SensorOnOff : MonoBehaviour
{
    public int touchCount;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("env"))
        {
            touchCount++;
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("env"))
        {
            touchCount--;
        }
    }

    public bool getState()
    {
        return touchCount != 0;
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = getState() ? Color.green : Color.red;
        Gizmos.DrawCube(
            transform.position,
            transform.lossyScale
            );
        Gizmos.color = Color.white;
    }
}
