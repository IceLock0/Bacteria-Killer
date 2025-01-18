using System;
using Configs.Upgrade;
using Enums.Upgrade;
using Model.Characters.Player.Upgrade;
using Services.Level;
using View.Characters.Player.Upgrade;

namespace Presenter.Character.Player.UpgradeBar
{
    public class PlayerUpgradePresenter
    {
        private readonly PlayerUpgradeModel _playerUpgradeModel;
        private readonly PlayerUpgradeView _playerUpgradeView;

        private readonly IPlayerUnspentLevelsProviderService _playerUnspentLevelsProviderService;

        public PlayerUpgradePresenter(PlayerUpgradeView playerUpgradeView, UpgradeType upgradeType,
            IPlayerUnspentLevelsProviderService playerUnspentLevelsProviderService, UpgradeConfig upgradeConfig)
        {
            _playerUpgradeModel = new PlayerUpgradeModel(upgradeType, upgradeConfig);
            _playerUpgradeView = playerUpgradeView;

            _playerUnspentLevelsProviderService = playerUnspentLevelsProviderService;
        }

        public event Action<UpgradeType, float> UpgradeCompleted; 
        
        public void OnEnable()
        {
            _playerUpgradeView.UpgradeClicked += _playerUpgradeModel.Upgrade;

            _playerUpgradeModel.Upgraded += OnUpgraded;

            _playerUnspentLevelsProviderService.LevelSpent += OnLevelSpent;
            _playerUnspentLevelsProviderService.LevelReceived += OnLevelReceived;
        }

        public void OnDisable()
        {
            _playerUpgradeView.UpgradeClicked -= _playerUpgradeModel.Upgrade;
            
            _playerUpgradeModel.Upgraded += OnUpgraded;
            
            _playerUnspentLevelsProviderService.LevelSpent += OnLevelSpent;
            _playerUnspentLevelsProviderService.LevelReceived += OnLevelReceived;
        }

        private void OnUpgraded(UpgradeType upgradeType, float value)
        {
            _playerUnspentLevelsProviderService.SpendLevel();

            _playerUpgradeView.AddUpgrade();
            
            UpgradeCompleted?.Invoke(upgradeType, value);
        }

        private void OnLevelSpent(int unspentLevels)
        {
            if (unspentLevels < 1 || _playerUpgradeModel.IsUpgradedMax())
                _playerUpgradeView.HideButton();
        }

        private void OnLevelReceived()
        {
            if (_playerUpgradeModel.IsUpgradedMax())
                return;
            
            _playerUpgradeView.ShowButton();
        }
    }
}