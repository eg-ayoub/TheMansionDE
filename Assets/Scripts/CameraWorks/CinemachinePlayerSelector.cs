using UnityEngine;
using Cinemachine;
using Player;
namespace CameraWorks
{
    public class CinemachinePlayerSelector : GameplayObject
    {
        private void Start()
        {
            GetComponent<CinemachineVirtualCamera>().Follow = PlayerInstanciationScript.playerTransform;
        }
    }
}