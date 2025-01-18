using Configs.Entities;
using Presenter.Character.Enemy;
using Services.Destroyer;
using Services.Movement.PositionProvider;
using Services.Upgrade;
using UnityEngine;
using Zenject;

namespace View.Characters.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyView : CharacterView
    {
        private IPlayerTransformProviderService _playerTransformProviderService;

        private EnemyConfig _enemyConfig;

        private EnemyPresenter _enemyPresenter;

        private IPlayerUpgradeProviderService _playerUpgradeProviderService;
        
        [Inject]
        public void Initialize(IPlayerTransformProviderService playerTransformProviderService, IPlayerUpgradeProviderService playerUpgradeProviderService)
        {
            _playerTransformProviderService = playerTransformProviderService;
            _playerUpgradeProviderService = playerUpgradeProviderService;
        }
        
        public void InitializeByFabric(EnemyConfig enemyConfig)
        {
            _enemyConfig = enemyConfig;
            CharacterConfig = _enemyConfig;

            HpView.Initialize(CharacterConfig, _playerUpgradeProviderService);

            _enemyPresenter = new EnemyPresenter(this, UpdaterService, _enemyConfig, Rigidobdy,
                _playerTransformProviderService, transform, HpView.Presenter, DamageableComponent,
                GameObjectDestroyerService);
            Presenter = _enemyPresenter;
        }
    }
}