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
    }
}