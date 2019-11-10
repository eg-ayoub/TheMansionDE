using UnityEngine;
using Player;
namespace CameraWorks
{
    public class CameraFollow : MonoBehaviour
    {
        float playerOffset;

        private void Start()
        {
            playerOffset = 3000;
        }

        private void Update()
        {
            transform.position =
                new Vector3(
                    PlayerInstanciationScript.playerTransform.position.x + playerOffset,
                    transform.position.y,
                    -10
                );
        }
    }
}