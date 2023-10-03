using UnityEngine;

namespace MoonActive.Scripts.Managers
{
    public class M_Audio : Singleton<M_Audio>
    {
        [SerializeField] private AudioSO _audioSo;

        AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = _audioSo.volume;
        }
    

        public void PlaySound(int index)
        {
            _audioSource.PlayOneShot(_audioSo._audioClips[index]);
        }
    
    }
}
