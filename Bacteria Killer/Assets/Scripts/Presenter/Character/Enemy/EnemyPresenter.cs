using Components.Damageable;
using Configs.Entities;
using Damagers.Enemy.Attacker;
using Model.Characters.Enemy;
using Presenter.HP;
using Services.Movement.DirectionProvider;
using Services.Movement.DirectionProvider.Enemy;
using Services.Movement.Mover;
using Services.Movement.PositionProvider;
using Services.Updater;
using UnityEngine;

namespace Presenter.Character.Enemy
{
    public class EnemyPresenter
    {
        private readonly EnemyModel _enemyModel;
        
        private readonly IUpdaterService _updaterService;

        private readonly IMoverService _moverService;
        private readonly IDirectionProviderService _directionProviderSerivce;

        private readonly IPlayerTransformProviderService _playerTransformProviderService;

        private readonly EnemyConfig _enemyConfig;
        private readonly Rigidbody2D _rigidbody;

        private readonly Transform _enemyTransform;

        private readonly EnemyAttacker _enemyAttacker;
        
        private readonly HPPresenter _hpPresenter;
        private readonly DamageableComponent _damageableComponent;

        public EnemyPresenter(IUpdaterService updaterService, EnemyConfig enemyConfig, Rigidbody2D rigidbody,
            IPlayerTransformProviderService playerTransformProviderService, Transform enemyTransform,  HPPresenter hpPresenter, DamageableComponent damageableComponent)
        {
            _enemyModel = new EnemyModel(enemyConfig);
            
            _updaterService = updaterService;

            _playerTransformProviderService = playerTransformProviderService;

            _enemyConfig = enemyConfig;
            _rigidbody = rigidbody;

            _directionProviderSerivce = new EnemyDirectionProviderService(_playerTransformProviderService.GetTransform(),
                enemyTransform, enemyConfig.AttackDistance);
            _moverService = new MoverService(_directionProviderSerivce, _enemyConfig.LinearSpeed, _rigidbody);

            _enemyAttacker = new EnemyAttacker(_playerTransformProviderService.GetTransform(), enemyTransform,
                _enemyModel.AttackDistance, _enemyModel.AttackDamage);
            
            _hpPresenter = hpPresenter;
            
            _damageableComponent = damageableComponent;
        }

        public void OnEnable()
        {
            _updaterService.FixedUpdated += FixedUpdate;
            _updaterService.Updated += Update;
            _damageableComponent.Damaged += TakeDamage;
        }

        public void OnDisable()
        {
            _updaterService.FixedUpdated -= FixedUpdate;
            _updaterService.Updated -= Update;
            _damageableComponent.Damaged -= TakeDamage;
        }

        public void TakeDamage(float value)
        {
            _hpPresenter.TakeDamage(value);
        }

        private void Update()
        {
            _enemyAttacker.Attack();
        }
        
        private void FixedUpdate()
        {
            _moverService.Move(Time.fixedDeltaTime);
        }
    }
}