using UnityEngine;

namespace MoonActive.Scripts.Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioSO audioSo;

        AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = audioSo.volume;
        }

        public void PlaySound(int index)
        {
            _audioSource.PlayOneShot(audioSo._audioClips[index]);
        }
    }
}
