using System;
using UnityEngine;
namespace Player
{
    namespace Movement
    {
        /// <summary>
        /// this monobehaviour stores data about ongoing collisions with:
        /// walls, ceilings, and floors.
        /// </summary>
        public class CollisionSensorScript : MonoBehaviour
        {
            /// <summary>
            /// up sensor
            /// </summary>
            [Header("Sensor Managers")]
            [SerializeField] IntermediateSensorManager up;

            /// <summary>
            /// down sensor
            /// </summary>
            [SerializeField] IntermediateSensorManager down;

            /// <summary>
            /// left sensor
            /// </summary>
            [SerializeField] IntermediateSensorManager left;

            /// <summary>
            /// right sensor
            /// </summary>
            [SerializeField] IntermediateSensorManager right;

            /// <summary>
            /// pause value.
            /// </summary>
            bool paused;

            /// <summary>
            /// object that stores info about collisions
            /// </summary>
            public CollisionInfo info;




            /// <summary>
            /// struct that stores info about Collisions
            /// </summary>
            public struct CollisionInfo
            {
                public bool left, right;
                public bool up, down;
                public Vector2 groundVector;

                public void Reset()
                {
                    left = right = false;
                    up = down = false;
                }

            }

            /// <summary>
            /// Start is called on the frame when a script is enabled just before
            /// any of the Update methods is called the first time.
            /// </summary>
            private void Start()
            {
                info.Reset();

            }
            /// <summary>
            /// Update is called every frame, if the MonoBehaviour is enabled.
            /// </summary>
            private void Update()
            {
                if (!paused)
                {
                    // info.Reset();
                    GetSensorValues();
                }
            }
            /// <summary>
            /// gets values of sensors from the sensor managers.
            /// </summary>
            private void GetSensorValues()
            {
                info.up = up.on;
                info.down = down.on;
                info.left = left.on;
                info.right = right.on;
            }
            /// <summary>
            /// pauses script
            /// </summary>
            public void OnPauseGame()
            {
                paused = true;
            }
            /// <summary>
            /// resumes script
            /// </summary>
            public void OnResumeGame()
            {
                paused = false;
            }

            internal void ResetAll()
            {
                up.ResetSensorArray();
                down.ResetSensorArray();
                left.ResetSensorArray();
                right.ResetSensorArray();
            }
        }
    }
}