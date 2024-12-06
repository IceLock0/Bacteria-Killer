using Configs.Entities;
using Damagers.Enemy.Attacker;
using Entities;
using Movement.Direction.Enemy;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class Enemy : Entity
    {
        [SerializeField] private Transform _playerTransform;

        private Attacker _attacker;
        
        [Inject]
        public void Initialize(EnemyConfig config)
        {
            Config = config;

            DirectionProvider = new EnemyDirectionProvider(_playerTransform, transform, config.AttackDistance);

            _attacker = new Attacker(_playerTransform, transform, config.AttackDistance, config.AttackDamage);
        }

        private void Update()
        {
            _attacker.Attack();
        }
    }
}