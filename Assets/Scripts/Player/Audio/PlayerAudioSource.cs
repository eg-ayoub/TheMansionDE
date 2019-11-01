using System;
using UnityEngine;
namespace Player.Audio
{
    public class PlayerAudioSource : GameplayObject
    {
        public enum CLIP { MANSION, MADNESS };
        public AudioClip mansionSoundtrack;
        public AudioClip madnessSoundtrack;
        AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Pause();
        }

        public void Play()
        {
            audioSource.Play();
        }

        public void Pause()
        {
            audioSource.Pause();
        }

        public void SetClip(CLIP clip)
        {
            switch (clip)
            {
                case CLIP.MANSION:
                    audioSource.clip = mansionSoundtrack;
                    break;
                case CLIP.MADNESS:
                    audioSource.clip = madnessSoundtrack;
                    break;
            }
        }

        internal void SetRunningVolume(float volume)
        {
            transform.GetChild(0).GetComponent<AudioSource>().volume = volume;
        }

        internal void PlayRunnigSoundEffect()
        {
            transform.GetChild(0).GetComponent<AudioSource>().Play();
        }

        internal void PauseRunningSoundEffect()
        {
            transform.GetChild(0).GetComponent<AudioSource>().Pause();
        }

        internal void PlayLandingSoundEffect()
        {
            transform.GetChild(1).GetComponent<AudioSource>().Play();
        }

        internal void PlayDeathEffect()
        {
            transform.GetChild(2).GetComponent<AudioSource>().Play();
        }


    }
}