using Services.Audio;
using UnityEngine;
using UnityEngine.UI;
using Utils.Extensions;
using Zenject;

namespace View.Buttons
{
    public abstract class BaseButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private IAudioService _audioService;
        private const string AUDIO_CLICK = "Click";
        
        [Inject]
        public void Initialize(IAudioService audioService)
        {
            _audioService = audioService;
        }

        protected abstract void OnClick();

        protected void PlayAudio()
        {
            _audioService.Play(AUDIO_CLICK);
        }
        
        private void OnEnable()
        {
            _button.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.RemoveListener(OnClick);
        }
    }
}