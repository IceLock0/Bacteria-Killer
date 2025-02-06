using UnityEngine;
using System.Collections.Generic;

namespace Services.Audio
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        [SerializeField] private List<AudioMap> _audio;
        
        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }
        
        public void Play(string key)
        {
            if (!TryFindAudio(key, out var audioMap))
                return;
            
            SetSourceParams(audioMap);
            
            _source.PlayOneShot(audioMap.AudioClip);
        }

        private bool TryFindAudio(string key, out AudioMap audioMap)
        {
            foreach (var audio in _audio)
            {
                if (audio.Key == key)
                {
                    audioMap = audio;
                    return true;
                }
            }
            
            Debug.LogError($"Audio with key {key} not found");
            audioMap = null;
            return false;
        }

        private void SetSourceParams(AudioMap audioMap)
        {
            var pitch = Random.Range(audioMap.MinPitch, audioMap.MaxPitch);

            _source.volume = audioMap.Volume;
            _source.pitch = pitch;
        }
    }
}