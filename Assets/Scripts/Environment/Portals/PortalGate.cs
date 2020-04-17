using UnityEngine;
using System.Collections;
namespace Environment.Portals
{
    public class PortalGate : GameplayObject
    {

        AudioSource audioSource;

        private WormHole wormHole;

        public bool active = true;
        public PortalGate Init(WormHole wormHole)
        {
            audioSource = GetComponent<AudioSource>();
            this.wormHole = wormHole;
            return this;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (active && !paused && other.CompareTag("Player"))
            {
                audioSource.Play();
                GetComponent<ParticleSystem>().Play();
                wormHole.Warp(this);
            }
        }

        public void Deactivate()
        {
            StartCoroutine(PostWarpDelay());
        }

        private IEnumerator PostWarpDelay()
        {
            active = false;
            for (int i = 0; i < 10; i++)
            {
                yield return null;
            }
            active = true;
        }
    }
}