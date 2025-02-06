using System.Collections.Generic;
using Configs.Entities;
using DG.Tweening;
using Presenter.Character.Enemy;
using Services.Movement.PositionProvider;
using Services.Upgrade;
using UnityEngine;
using Zenject;

namespace View.Characters.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyView : CharacterView
    {
        [SerializeField] private List<SpriteRenderer> _bloodPools;

        private IPlayerTransformProviderService _playerTransformProviderService;

        private EnemyPresenter _enemyPresenter;

        private IPlayerUpgradeProviderService _playerUpgradeProviderService;

        private const string AUDIO_KILL_ENEMY = "KillEnemy";
        
        public EnemyConfig EnemyConfig;
        
        [Inject]
        public void Initialize(IPlayerTransformProviderService playerTransformProviderService, IPlayerUpgradeProviderService playerUpgradeProviderService)
        {
            _playerTransformProviderService = playerTransformProviderService;
            _playerUpgradeProviderService = playerUpgradeProviderService;
        }
        
        public void InitializeByFabric(EnemyConfig enemyConfig, float bossScaler)
        {
            EnemyConfig = enemyConfig;
            CharacterConfig = EnemyConfig;

            HpView.Initialize(CharacterConfig, _playerUpgradeProviderService);
            
            _enemyPresenter = new EnemyPresenter(this, bossScaler, UpdaterService, EnemyConfig, Rigidobdy,
                _playerTransformProviderService, transform, HpView.Presenter, DamageableComponent,
                GameObjectDestroyerService);
            Presenter = _enemyPresenter;

            if (bossScaler > 1)
            {
                gameObject.transform.localScale *= bossScaler;
                var sprite = GetComponentInChildren<SpriteRenderer>();
                sprite.color = Color.red;
            }
        }

        public override void ShowDeath()
        {
            AudioService.Play(AUDIO_KILL_ENEMY);
            
            var rndBloodPoolIndex = Random.Range(0, _bloodPools.Count);
            SpriteRenderer bloodPool = _bloodPools[rndBloodPoolIndex];

            SpriteRenderer createdSprite = Instantiate(bloodPool, transform.position, Quaternion.identity);

            var tween = createdSprite.DOFade(0,2);

            tween.OnComplete(() => 
            {
                tween.Kill();
                Destroy(createdSprite.gameObject);
            });
        }
    }
}