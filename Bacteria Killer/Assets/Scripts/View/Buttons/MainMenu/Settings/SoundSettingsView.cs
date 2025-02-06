using System;
using Data;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Utils.SaveKeys;
using Zenject;

namespace View.MainMenuButtons
{
    public class SoundSettingsView : MonoBehaviour
    {
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private AudioMixer _mixer;
        
        private ISaveLoadService _saveLoadService;
        
        private const string MIXER_NAME = "Master";

        [Inject]
        public void Initialize(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        private void Start()
        {
            Load();
        }

        private void SetVolume(float value)
        {
            _mixer.SetFloat(MIXER_NAME, Mathf.Lerp(-80.0f, 0.0f, value));
        }

        private void SetSlider(float value)
        {
            _volumeSlider.value = value;
        }

        private void OnEnable()
        {
            _volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        private void OnDisable()
        {
            _volumeSlider.onValueChanged.RemoveListener(SetVolume);
        }

        private void Load()
        {
            var data = _saveLoadService.Load<SettingsData>(SaveKeys.SETTINGS);
            var loadedVolume = data == null ? 0.5f : data.Volume;

            SetSlider(loadedVolume);
            SetVolume(_volumeSlider.value);
        }
    }
}