using UnityEngine;

namespace CameraWorks
{
    public class CameraShaker : MonoBehaviour
    {
        public void Shake()
        {
            GetComponent<Animator>().SetTrigger("Shake");
        }
    }
}