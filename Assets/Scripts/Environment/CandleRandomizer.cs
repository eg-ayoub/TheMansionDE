using UnityEngine;

public class CandleRandomizer : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Animator>().speed = .7f + Random.Range(.0f, 1f) * .3f;
    }
}