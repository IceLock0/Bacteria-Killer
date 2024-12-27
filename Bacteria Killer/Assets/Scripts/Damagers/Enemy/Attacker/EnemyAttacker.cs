using System.Threading.Tasks;
using Components.Damageable;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Damagers.Enemy.Attacker
{
    public class EnemyAttacker
    {
        private readonly Transform _player;
        private readonly Transform _at;
        
        private readonly float _attackDistance;
        private readonly float _damage;

        private bool _isCooldownExpired = true;
        
        public EnemyAttacker(Transform player, Transform at, float attackDistance, float damage)
        {
            _player = player;
            _at = at;
            
            _attackDistance = attackDistance;
            _damage = damage;
        }
        
        public void Attack()
        {
            if (_player == null || !IsDistanceNear() || !_player.TryGetComponent(out IDamageable damageable) || !_isCooldownExpired)
                return;
            
            damageable.TakeDamage(_damage);
            _isCooldownExpired = false;
            WaitNextFire().Forget();
        }

        private bool IsDistanceNear() 
            => Vector2.Distance(_player.position, _at.position) <= _attackDistance;
        
        private async UniTaskVoid WaitNextFire()
        {
            await Task.Delay((int) (0.25 * 1000));
            _isCooldownExpired = true;
        }
    }
}