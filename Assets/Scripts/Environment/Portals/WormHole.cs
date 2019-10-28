using UnityEngine;
using Player;
namespace Environment.Portals
{
    public class WormHole : GameplayObject
    {
        private PortalGate firstGate;
        private PortalGate secondGate;

        private void Start()
        {
            firstGate = GetComponentsInChildren<PortalGate>()[0].Init(this);
            secondGate = GetComponentsInChildren<PortalGate>()[1].Init(this);
        }

        public void Warp(PortalGate from)
        {
            PortalGate to = (from == firstGate ? secondGate : firstGate);
            to.Deactivate();
            PlayerInstanciationScript.playerTransform.position = to.transform.position;
        }
    }
}