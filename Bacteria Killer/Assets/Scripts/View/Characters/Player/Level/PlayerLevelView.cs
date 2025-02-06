using Configs.Level;
using Presenter.Character.Player.Level;
using Services.Audio;
using Services.Destroyer;
using Services.Level;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Characters.Player.Level
{
    public class PlayerLevelView : MonoBehaviour
    {
        [SerializeField] private Image _xpImage;
        
        private ParticleSystem _particleSystem;

        private PlayerLevelPresenter _playerLevelPresenter;

        private IPlayerUnspentLevelsProviderService _playerUnspentLevelsProviderService;

        private IAudioService _audioService;
        private const string AUDIO_LEVEL_UP = "LevelUp";
        
        private bool _isLevelUpVisible = false;
        
        [Inject]
        public void Initialize(PlayerLevelConfig playerLevelConfig,
            IGameObjectDestroyerService gameObjectDestroyerService,
            IPlayerUnspentLevelsProviderService playerUnspentLevelsProviderService, IAudioService audioService)
        {
            _playerUnspentLevelsProviderService = playerUnspentLevelsProviderService;
            
            _playerLevelPresenter = new PlayerLevelPresenter(this, playerLevelConfig, gameObjectDestroyerService, _playerUnspentLevelsProviderService);

            _particleSystem = GetComponentInChildren<ParticleSystem>();
            
            _audioService = audioService;
        }

        public void UpdateXp(float current, float target)
        {
            _xpImage.fillAmount = current / target;
        }

        public void ShowLevelUp()
        {
            _audioService.Play(AUDIO_LEVEL_UP);
            
            if (_isLevelUpVisible)
                return;
            
            _particleSystem.Play();
            
            _isLevelUpVisible = true;
        }

        public void HideLevelUp()
        {
            _particleSystem.Stop();

            _isLevelUpVisible = false;
        }

        private void Awake()
        {
            SetDefault();
        }

        private void OnEnable()
        {
            _playerLevelPresenter.OnEnable();
        }

        private void OnDisable()
        {
            _playerLevelPresenter.OnDisable();
        }

        private void SetDefault()
        {
            _xpImage.fillAmount = 0.0f;
            _xpImage.gameObject.SetActive(true);

            _particleSystem.Stop();
        }
    }
}