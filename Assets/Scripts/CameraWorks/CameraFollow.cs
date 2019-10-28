using UnityEngine;
using Player;
namespace CameraWorks
{
    public class CameraFollow : MonoBehaviour
    {
        float playerOffset;

        private void Start()
        {
            playerOffset = -PlayerInstanciationScript.playerTransform.position.x + transform.position.x;
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