using UnityEngine;

namespace Services.Movement.DirectionProvider.Enemy
{
    public class EnemyDirectionProviderService : IDirectionProviderService
    {
        private readonly Transform _playerTransform;
        private readonly Transform _enemyTransform;

        private readonly float _attackDistance;

        public EnemyDirectionProviderService(Transform playerTransform, Transform enemyTransform, float attackDistance)
        {
            _playerTransform = playerTransform;
            _enemyTransform = enemyTransform;

            _attackDistance = attackDistance;
        }

        public Vector2 GetDirection()
        {
            if (_playerTransform == null || IsPlayerClose())
                return Vector2.zero;

            return (_playerTransform.position - _enemyTransform.position).normalized;
        }

        private bool IsPlayerClose() =>
            Vector2.Distance(_playerTransform.position, _enemyTransform.position) <= _attackDistance;
    }
}