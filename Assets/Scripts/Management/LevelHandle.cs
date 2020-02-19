using UnityEngine;

namespace Management
{
    public abstract class LevelHandle : MonoBehaviour
    {
        public int buildIndex;

        public bool isCheckpoint;

        public Transform spawnpoint;

        private void Start()
        {
            spawnpoint = transform.GetChild(0);
        }
    }
}