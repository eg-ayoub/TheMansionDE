using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffector : MonoBehaviour
{
    AudioSource audioSource;
    ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particles = GetComponent<ParticleSystem>();
    }

    public void Play()
    {
        particles.Play();
        audioSource.Play();
    }
}
