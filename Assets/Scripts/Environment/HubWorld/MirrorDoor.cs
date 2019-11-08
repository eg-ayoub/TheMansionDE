using UnityEngine;
namespace Environment.HubWorld
{
    public class MirrorDoor : GameplayObject
    {
        Camera cam;
        public void Start()
        {
            cam = GetComponentInChildren<Camera>();
            Matrix4x4 mat = cam.projectionMatrix;
            mat *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
            cam.projectionMatrix = mat;
        }
    }
}