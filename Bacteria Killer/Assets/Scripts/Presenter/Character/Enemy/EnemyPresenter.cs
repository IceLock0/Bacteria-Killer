using Components.Damageable;
using Configs.Entities;
using Damagers.Enemy.Attacker;
using Model.Characters.Enemy;
using Presenter.HP;
using Services.Destroyer;
using Services.Movement.DirectionProvider.Enemy;
using Services.Movement.Mover;
using Services.Movement.PositionProvider;
using Services.Updater;
using Services.Upgrade;
using UnityEngine;
using View.Characters.Enemy;

namespace Presenter.Character.Enemy
{
    public class EnemyPresenter : CharacterPresenter
    {
        private readonly EnemyModel _enemyModel;

        private readonly IUpdaterService _updaterService;

        private readonly EnemyAttacker _enemyAttacker;

        public EnemyPresenter(EnemyView enemyView, IUpdaterService updaterService, EnemyConfig enemyConfig,
            Rigidbody2D rigidbody,
            IPlayerTransformProviderService playerTransformProviderService, Transform enemyTransform,
            HPPresenter hpPresenter, DamageableComponent damageableComponent,
            IGameObjectDestroyerService gameObjectDestroyerService)
            : base(enemyView, updaterService, hpPresenter, damageableComponent, gameObjectDestroyerService)
        {
            _enemyModel = new EnemyModel(enemyConfig);
            CharacterModel = _enemyModel;

            _updaterService = updaterService;

            var enemyDirectionProviderService = new EnemyDirectionProviderService(
                playerTransformProviderService.GetTransform(),
                enemyTransform, enemyConfig.AttackDistance);
            MoverService = new MoverService(enemyDirectionProviderService, rigidbody);

            _enemyAttacker = new EnemyAttacker(playerTransformProviderService.GetTransform(), enemyTransform,
                _enemyModel.AttackDistance, _enemyModel.AttackDamage);
        }

        public override void OnEnable()
        {
            base.OnEnable();
            
            _updaterService.Updated += Update;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            
            _updaterService.Updated -= Update;
        }

        private void Update()
        {
            _enemyAttacker.Attack();
        }
    }
}