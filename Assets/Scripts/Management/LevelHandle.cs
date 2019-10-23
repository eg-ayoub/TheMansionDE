using UnityEngine;

namespace Management
{
    public class LevelHandle : MonoBehaviour
    {
        public int buildIndex;

        public CHECKPOINTS checkpoint;

        public Transform spawnpoint;

        private void Start()
        {
            spawnpoint = transform.GetChild(0);
        }
    }
}