using UnityEngine;
using View.Buttons;

namespace View.MainMenuButtons
{
    public class SetiingsButtonView : BaseButton
    {
        [SerializeField] private GameObject _settings;

        private void Awake()
        {
            gameObject.SetActive(true);
        }

        protected override void OnClick()
        {
            PlayAudio();
            OpenSettings();
        }
        
        private void OpenSettings()
        {
            _settings.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}