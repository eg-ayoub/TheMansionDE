using System.Collections;
using UnityEngine;
namespace Player.Movement
{
    /// <summary>
    /// this script manages normal physics (gravity, friction ...)
    /// and clamping the speed 
    /// </summary>
    public class PlayerPhysics : GameplayObject
    {
        /// <summary>
        /// target speed taken from the movement manager
        /// </summary>
        Vector2 targetSpeed;
        /// <summary>
        /// gravity acting on the player
        /// </summary>
        [SerializeField, Header("Physics values")]
        float gravity;
        /// <summary>
        /// deltaposition calculated from speed
        /// </summary>
        Vector2 deltaPosition;
        /// <summary>
        /// max horizontal speed of the player
        /// </summary>
        [SerializeField, Header("Clamping values")]
        int maxSpeedX;
        /// <summary>
        /// max vertical speed of the player
        /// </summary>
        [SerializeField] int maxSpeedY;
        /// <summary>
        /// player's movement modifier
        /// </summary>
        PlayerMovementModifier movementModifier;
        /// <summary>
        /// player's wall, ground and ceil sensor
        /// </summary>
        CollisionSensorScript sensor;
        /// <summary>
        /// player's clip manager
        /// </summary>
        PlayerClipManager clipManager;


        void Start()
        {
            movementModifier = GetComponentInParent<PlayerMovementModifier>();
            sensor = GetComponentInChildren<CollisionSensorScript>();
            clipManager = GetComponent<PlayerClipManager>();
            paused = false;
        }

        private void FixedUpdate()
        {
            if (!paused)
            {
                if (!sensor.info.down)
                {
                    targetSpeed -= gravity * Time.fixedDeltaTime * Vector2.up;
                }
                ClampSpeed(maxSpeedX, maxSpeedY);
                movementModifier.SetSpeed(targetSpeed);
                deltaPosition = targetSpeed * Time.fixedDeltaTime;
                clipManager.SetDeltaPosition(deltaPosition);
                if (deltaPosition.magnitude >= .5f)
                {
                    movementModifier.moving = true;
                }
                else
                {
                    movementModifier.moving = false;
                }
            }
        }
        /// <summary>
        /// clamps the speed vector coordinates under max_x and max_y
        /// </summary>
        /// <param name="max_x">max speed horizontal</param>
        /// <param name="max_y">max speed vertical</param>
        private void ClampSpeed(float max_x, float max_y)
        {
            targetSpeed.x = Mathf.Sign(targetSpeed.x) * Mathf.Clamp(Mathf.Abs(targetSpeed.x), 0, max_x);
            targetSpeed.y = Mathf.Sign(targetSpeed.y) * Mathf.Clamp(Mathf.Abs(targetSpeed.y), 0, max_y);
        }
        /// <summary>
        /// setter for this script's speed
        /// </summary>
        /// <param name="speed">new speed</param>
        public void SetTargetSpeed(Vector2 speed)
        {
            targetSpeed = speed;
        }
        /// <summary>
        /// freeze's the player's movements momentarily
        /// </summary>
        public void Freeze()
        {
            StartCoroutine(FreezeMovement());
        }
        /// <summary>
        /// coroutine that freeze's the player's movements for 10 frames
        /// </summary>
        /// <returns></returns>
        IEnumerator FreezeMovement()
        {
            for (int i = 0; i < 10; i++)
            {
                targetSpeed = Vector2.zero;
                yield return null;
            }

        }
        /// <summary>
        /// setter for gravity
        /// </summary>
        /// <param name="g">new gravity constant</param>
        public void SetGravity(float g)
        {
            gravity = g;
        }
        public float GetGravity()
        {
            return gravity;
        }
    }
}
