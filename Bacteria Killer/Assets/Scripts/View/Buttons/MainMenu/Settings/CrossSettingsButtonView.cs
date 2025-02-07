using Data;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Utils.SaveKeys;
using View.Buttons;
using Zenject;

namespace View.MainMenuButtons
{
    public class CrossSettingsButtonView : BaseButton
    {
        [SerializeField] private GameObject _buttonGameObject;
        [SerializeField] private Slider _soundSlider;
        
        private ISaveLoadService _saveLoadService;
        
        [Inject]
        public void Initialize(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        protected override void OnClick()
        {
            PlayAudio();
            CloseSettings();
            Save();
        }

        private void Awake()
        {
            gameObject.SetActive(false);
        }
        
        private void CloseSettings()
        { 
            _buttonGameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void Save()
        {
            var data = new SettingsData(){Volume = _soundSlider.value};
            _saveLoadService.Save(SaveKeys.SETTINGS, data);
        }
    }
}