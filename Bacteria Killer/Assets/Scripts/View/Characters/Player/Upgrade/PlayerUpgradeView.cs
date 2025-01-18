using System;
using System.Collections.Generic;
using Configs.Upgrade;
using Enums.Upgrade;
using Presenter.Character.Player.UpgradeBar;
using Services.Level;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Characters.Player.Upgrade
{
    public class PlayerUpgradeView : MonoBehaviour
    {
        [SerializeField] private UpgradeType _upgradeType;
        [SerializeField] private Image _filledUpgradeImagePrefab;
        [SerializeField] private List<Image> _sourceImages;

        private Button _upgradeButton;

        private IPlayerUnspentLevelsProviderService _playerUnspentLevelsProviderService;

        private int _currentImageIndex = 0;
        

        [Inject]
        public void Initialize(IPlayerUnspentLevelsProviderService playerUnspentLevelsProviderService)
        {
            _playerUnspentLevelsProviderService = playerUnspentLevelsProviderService;
            
            _upgradeButton = GetComponentInChildren<Button>();
            HideButton();
        }

        public void InitializeByFabric(UpgradeConfig upgradeConfig)
        {
            PlayerUpgradePresenter = new PlayerUpgradePresenter(this, _upgradeType, _playerUnspentLevelsProviderService, upgradeConfig);
        }
        
        public PlayerUpgradePresenter PlayerUpgradePresenter { get; private set; }
        
        public UpgradeType UpgradeType => _upgradeType;
        
        public event Action UpgradeClicked;
            
        public void AddUpgrade()
        {
            if (_currentImageIndex >= _sourceImages.Count)
            {
                Debug.LogWarning($"Visual upgrade is max. Lookup the config with type = {_upgradeType}");
                return;
            }

            _sourceImages[_currentImageIndex].sprite = _filledUpgradeImagePrefab.sprite;
            _currentImageIndex++;
        }
        
        public void HideButton()
        {
            _upgradeButton.gameObject.SetActive(false);
        }    
        
        public void ShowButton()
        {
            _upgradeButton.gameObject.SetActive(true);
        }

        private void OnUpgradeClicked()
        {
            UpgradeClicked?.Invoke();
        }

        private void OnEnable()
        {
            _upgradeButton.onClick.AddListener(OnUpgradeClicked);
            
            PlayerUpgradePresenter.OnEnable();
        }
        
        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveListener(OnUpgradeClicked);
            
            PlayerUpgradePresenter.OnDisable();
        }
    }
}