using UnityEngine;
using Player;
namespace Environment.HubWorld
{
    public class Teleporter : GameplayObject
    {
        Transform madnessEnd, normalEnd;
        private void Start()
        {
            normalEnd = transform.GetChild(0);
            madnessEnd = transform.GetChild(1);
        }

        public void GoToMadness()
        {
            PlayerInstanciationScript.playerTransform.position = madnessEnd.position;
        }

        public void GoToNormal()
        {
            PlayerInstanciationScript.playerTransform.position = normalEnd.position;
        }
    }
}