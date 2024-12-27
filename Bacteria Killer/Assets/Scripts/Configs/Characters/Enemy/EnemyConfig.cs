using UnityEngine;

namespace Configs.Entities
{
    [CreateAssetMenu(fileName = "Enemy Config", menuName = "Configs/Entities/Enemy", order = 0)]
    public class EnemyConfig : CharacterConfig
    {
        [SerializeField] private float _attackDistance;

        [SerializeField] private float _attackDamage;

        [SerializeField] private float _difficult;
        
        public float AttackDistance => _attackDistance;

        public float AttackDamage => _attackDamage;

        public float Difficult => _difficult;
    }
}