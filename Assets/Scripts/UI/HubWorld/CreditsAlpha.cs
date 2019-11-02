using UnityEngine;
using UnityEngine.UI;
using Player;

public class CreditsAlpha : MonoBehaviour
{
    Text[] names;

    const float minDistance = 200f;
    const float maxDistance = 2000f;

    private void Start()
    {
        names = GetComponentsInChildren<Text>();
    }

    private void Update()
    {
        float distance = transform.position.x - PlayerInstanciationScript.playerTransform.position.x;
        distance = Mathf.Abs(distance);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        distance = Mathf.InverseLerp(minDistance, maxDistance, distance);
        foreach (Text name in names)
        {
            name.color = new Color(1, 1, 1, 1 - distance);
        }
    }
}