using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSO", menuName = "Data/Audio")]
public class AudioSO : ScriptableObject
{
    public List<AudioClip> _audioClips = new List<AudioClip>();
    public float volume = .5f;

}