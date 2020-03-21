using UnityEngine;
using System.Collections;
using Player;
namespace Environment.Hazards
{
    public class DamageStream : GameplayObject
    {
        public float startDelaySeconds;
        public LayerMask layerMask;
        const float delay = 2f;
        ParticleSystem particles;

        private void Start()
        {
            particles = GetComponent<ParticleSystem>();
            StartCoroutine(InitLoop());
        }

        IEnumerator InitLoop()
        {
            yield return new WaitForSeconds(startDelaySeconds);
            StartCoroutine(Loop());
        }

        IEnumerator Loop()
        {
            AudioSource source = GetComponent<AudioSource>();
            while (true)
            {
                source.Pause();
                yield return new WaitForSeconds(delay);

                particles.Play();
                source.Play();

                yield return null;

                while (particles.particleCount != 0)
                {
                    // * cast from 3 different positions
                    RaycastHit2D hit1, hit2, hit3;
                    hit1 = Physics2D.Raycast(150 * transform.up + transform.position, transform.right, 10000f, layerMask);
                    hit2 = Physics2D.Raycast(transform.position, transform.right, 10000f, layerMask);
                    hit3 = Physics2D.Raycast(-150 * transform.up + transform.position, transform.right, 10000f, layerMask);
                    if (hit1 || hit2 || hit3)
                    {

                        PlayerInstanciationScript.hpManager.TakeDamage();
                    }
                    yield return null;
                }
            }
        }
    }
}