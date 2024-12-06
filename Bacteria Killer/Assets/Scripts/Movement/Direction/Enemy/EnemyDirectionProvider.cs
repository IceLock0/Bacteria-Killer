using UnityEngine;

namespace Movement.Direction.Enemy
{
    public class EnemyDirectionProvider : IDirectionProvider
    {
        private readonly Transform _player;
        private readonly Transform _at;

        private readonly float _stopDistance;
        
        public EnemyDirectionProvider(Transform player, Transform at, float stopDistance)
        {
            _player = player;
            _at = at;

            _stopDistance = stopDistance;
        }
        
        public Vector2 GetDirection()
        {
            if (IsPlayerClose())
                return Vector2.zero;

            return (_player.position - _at.position).normalized;
        }

        private bool IsPlayerClose() => Vector3.Distance(_player.position, _at.position) <= _stopDistance;
    }
}