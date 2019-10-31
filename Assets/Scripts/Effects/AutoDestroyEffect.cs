using UnityEngine;

namespace Effects
{
    public class AutoDestroyEffect : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 2f);
        }
    }
}