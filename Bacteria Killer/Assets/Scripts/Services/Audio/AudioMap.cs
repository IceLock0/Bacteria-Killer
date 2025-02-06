using System;
using UnityEngine;

namespace Services.Audio
{
    [Serializable]
    public class AudioMap
    {
        [SerializeField] private string _key;
        [SerializeField] private AudioClip _audioClip;
        
        [SerializeField] private float _volume;
        [SerializeField] private float _minPitch;
        [SerializeField] private float _maxPitch;
        
        public string Key => _key;
        public AudioClip AudioClip => _audioClip;
        
        public float Volume => _volume;
        public float MinPitch => _minPitch;
        public float MaxPitch => _maxPitch;
    }
}