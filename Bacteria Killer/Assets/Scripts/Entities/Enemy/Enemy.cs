using Configs.Entities;
using Entities;
using Movement.Enemy;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class Enemy : Entity
    {
        [SerializeField] private Transform _playerTransform;
        
        [Inject]
        public void Initialize(EnemyConfig config)
        {
            Config = config;

            DirectionProvider = new EnemyDirectionProvider(_playerTransform, transform);
        }
    }
}