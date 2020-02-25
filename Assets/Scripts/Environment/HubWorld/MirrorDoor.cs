using UnityEngine;
using Management;
using UnityEngine.Experimental.Rendering.Universal;
using InputManagement;
using Player;
namespace Environment.HubWorld
{
    public class MirrorDoor : GameplayObject
    {
        Camera cam;

        Teleporter teleporter;

        SpriteRenderer indicator;

        public void Start()
        {
            cam = GetComponentInChildren<Camera>();
            Matrix4x4 mat = cam.projectionMatrix;
            mat *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
            cam.projectionMatrix = mat;

            indicator = transform.GetChild(5).GetComponent<SpriteRenderer>();

            foreach (Light2D mLight in transform.GetChild(4).GetComponentsInChildren<Light2D>())
            {
                mLight.enabled = false;
            }

            // transform.GetChild(4).GetChild(1).GetComponent<SpriteRenderer>().sprite = candleOff;

            // foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
            // {
            //     renderer.sortingLayerName = "Mirror";
            // }
        }

        private void Update()
        {
            if (teleporter.accessible)
            {
                float distance = transform.position.x - PlayerInstanciationScript.playerTransform.position.x;
                distance = Mathf.Abs(distance);
                distance = Mathf.Clamp(distance, 100, 1000);
                distance = Mathf.InverseLerp(100, 1000, distance);
                indicator.color = new Color(1, 1, 1, 1 - distance);
            }
            else
            {
                indicator.color = Color.clear;
            }
        }

        public void SetTeleporter(Teleporter teleporter)
        {
            this.teleporter = teleporter;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !paused && teleporter.accessible)
            {
                if (KeyMapper.GetButtonDown("Start"))
                {
                    Enter();
                }
            }
        }

        public void Enter()
        {
            teleporter.Enter(this);
        }

        public void DecorateAccessible()
        {
            foreach (Light2D mLight in transform.GetChild(4).GetComponentsInChildren<Light2D>())
            {
                mLight.enabled = true;
            }
        }
    }
}