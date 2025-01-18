using Configs.Level;
using Presenter.Character.Player.Level;
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

        private bool _isLevelUpVisible = false;
        
        [Inject]
        public void Initialize(PlayerLevelConfig playerLevelConfig,
            IGameObjectDestroyerService gameObjectDestroyerService,
            IPlayerUnspentLevelsProviderService playerUnspentLevelsProviderService)
        {
            _playerUnspentLevelsProviderService = playerUnspentLevelsProviderService;
            
            _playerLevelPresenter = new PlayerLevelPresenter(this, playerLevelConfig, gameObjectDestroyerService, _playerUnspentLevelsProviderService);

            _particleSystem = GetComponentInChildren<ParticleSystem>();
        }

        public void UpdateXp(float current, float target)
        {
            _xpImage.fillAmount = current / target;
        }

        public void ShowLevelUp()
        {
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