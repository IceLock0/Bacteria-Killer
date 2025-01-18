using Configs.Level;
using Model.Characters.Player.Level;
using Services.Destroyer;
using Services.Level;
using UnityEngine;
using View.Characters.Enemy;
using View.Characters.Player.Level;

namespace Presenter.Character.Player.Level
{
    public class PlayerLevelPresenter
    {
        private readonly PlayerLevelModel _playerLevelModel;
        private readonly PlayerLevelView _playerLevelView;

        private readonly IGameObjectDestroyerService _gameObjectDestroyerService;
        
        private readonly IPlayerUnspentLevelsProviderService _playerUnspentLevelsProviderService;

        public PlayerLevelPresenter(PlayerLevelView playerLevelView, PlayerLevelConfig playerLevelConfig,
            IGameObjectDestroyerService gameObjectDestroyerService,
            IPlayerUnspentLevelsProviderService playerUnspentLevelsProviderService)
        {
            _playerLevelView = playerLevelView;

            _playerLevelModel = new PlayerLevelModel(playerLevelConfig);

            _gameObjectDestroyerService = gameObjectDestroyerService;
            _playerUnspentLevelsProviderService = playerUnspentLevelsProviderService;
        }

        public void OnEnable()
        {
            _gameObjectDestroyerService.Destroyed += HandleEnemyDestroy;
            
            _playerLevelModel.XpChanged += _playerLevelView.UpdateXp;
            _playerLevelModel.Upped += _playerLevelView.ShowLevelUp;
            _playerLevelModel.Upped += _playerUnspentLevelsProviderService.ReceiveLevel;

            _playerUnspentLevelsProviderService.LevelSpent += OnLevelSpent;
        }

        public void OnDisable()
        {
            _gameObjectDestroyerService.Destroyed -= HandleEnemyDestroy;
            _playerLevelModel.XpChanged -= _playerLevelView.UpdateXp;
            _playerLevelModel.Upped -= _playerLevelView.ShowLevelUp;
            _playerLevelModel.Upped -= _playerUnspentLevelsProviderService.ReceiveLevel;
            
            _playerUnspentLevelsProviderService.LevelSpent += OnLevelSpent;
        }

        private void AddXp(float value)
        {
            _playerLevelModel.AddXp(value);
        }

        private void OnLevelSpent(int unspentLevels)
        {
            if(unspentLevels < 1)
                _playerLevelView.HideLevelUp();
        }
        
        private void HandleEnemyDestroy(GameObject gameObject)
        {
            if (!gameObject.TryGetComponent<EnemyView>(out _))
                return;

            AddXp(20);
        }
    }
}