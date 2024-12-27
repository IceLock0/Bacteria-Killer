using Components.Damageable;
using Configs.Entities;
using Presenter.Character.Enemy;
using Services.Movement.PositionProvider;
using Services.Updater;
using UnityEngine;
using View.HP;
using Zenject;

namespace View
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyView : MonoBehaviour
    {
        private IUpdaterService _updaterService;
        private IPlayerTransformProviderService _playerTransformProviderService;
        
        private EnemyConfig _enemyConfig;

        private Rigidbody2D _rb;
        
        private EnemyPresenter _enemyPresenter;
        
        private HPView _hpView;
        
        [Inject]
        public void Initialize(IUpdaterService updaterService, IPlayerTransformProviderService playerTransformProviderService)
        {
            _updaterService = updaterService;
            _playerTransformProviderService = playerTransformProviderService;
            
            _rb = GetComponent<Rigidbody2D>();
            _rb.gravityScale = 0.0f;
            _rb.freezeRotation = true;
        }

        public void InitializeByFabric(EnemyConfig enemyConfig)
        {
            _enemyConfig = enemyConfig;
            
            _hpView = GetComponentInChildren<HPView>();
            _hpView.Initialize(_enemyConfig);

            var damageableComponent = GetComponent<DamageableComponent>();
            
            _enemyPresenter = new EnemyPresenter(_updaterService, _enemyConfig, _rb, _playerTransformProviderService, transform, _hpView.Presenter, damageableComponent);
        }

        private void OnEnable()
        {
            _enemyPresenter.OnEnable();
        }

        private void OnDisable()
        {
            _enemyPresenter.OnDisable();
        }
    }
}