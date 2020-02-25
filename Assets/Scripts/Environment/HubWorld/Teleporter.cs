using UnityEngine;
using Player;
using System.Collections;
namespace Environment.HubWorld
{
    public class Teleporter : GameplayObject
    {
        public bool accessible;
        public bool timedOut;
        MirrorDoor madnessEnd, normalEnd;
        private void Start()
        {
            normalEnd = transform.GetChild(0).GetComponent<MirrorDoor>();
            madnessEnd = transform.GetChild(1).GetComponent<MirrorDoor>();

            normalEnd.SetTeleporter(this);
            madnessEnd.SetTeleporter(this);
        }

        public void GoToMadness()
        {
            PlayerInstanciationScript.playerTransform.position = madnessEnd.transform.position;
        }

        public void GoToNormal()
        {
            PlayerInstanciationScript.playerTransform.position = normalEnd.transform.position;
        }

        public void Init()
        {
            if (accessible)
            {
                normalEnd.DecorateAccessible();
                madnessEnd.DecorateAccessible();
            }
        }

        public void Enter(MirrorDoor end)
        {
            if (!timedOut && end == normalEnd)
            {
                GoToMadness();
                StartCoroutine(TeleportationTimeout());
                // TODO : sound effect for transition ...
                // TODO 2 : maybe change the hub music ? ...
            }
            else if (!timedOut && end == madnessEnd)
            {
                GoToNormal();
                StartCoroutine(TeleportationTimeout());
                // TODO : sound effect for transition ...
                // TODO 2 : maybe change the hub music ? ...
            }
        }

        public IEnumerator TeleportationTimeout()
        {
            timedOut = true;
            yield return new WaitForSecondsRealtime(1f);
            timedOut = false;
            yield return null;
        }
    }
}