using Configs.Entities;
using Enemy.Components;
using Entities.Components;
using Movement;
using Movement.Enemy;
using UnityEngine;
using UnityEngine.UI;
using View.HP;
using Zenject;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private Image _hpImage;

        [SerializeField] private Transform _playerTransform;

        private HPComponent _hp;
        private HPView _hpView;

        private Mover _mover;

        [Inject]
        public void Initialize(EnemyConfig enemyConfig)
        {
            _hp = new HPComponent(enemyConfig.HP);
            _hpView = new HPView(_hp, _hpImage, enemyConfig.HP);

            _mover = new Mover(new EnemyDirectionProvider(_playerTransform, transform), enemyConfig,
                GetComponent<Rigidbody2D>());
        }

        public void TakeDamage(float value)
        {
            _hp.TakeDamage(value);
        }

        private void FixedUpdate()
        {
            _mover.Move(Time.fixedDeltaTime);
        }
    }
}