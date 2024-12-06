using UnityEngine;

namespace Configs.Entities
{
    [CreateAssetMenu(fileName = "Enemy Config", menuName = "Configs/Entities/Enemy", order = 0)]
    public class EnemyConfig : EntityConfig
    {
        [SerializeField] private float _attackDistance;

        [SerializeField] private float _attackDamage;
        
        public float AttackDistance => _attackDistance;

        public float AttackDamage => _attackDamage;
    }
}