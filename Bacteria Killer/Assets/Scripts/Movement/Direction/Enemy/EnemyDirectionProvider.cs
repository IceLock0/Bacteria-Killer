using UnityEngine;

namespace Movement.Enemy
{
    public class EnemyDirectionProvider : IDirectionProvider
    {
        private readonly Transform _player;
        private readonly Transform _at;
        
        public EnemyDirectionProvider(Transform player, Transform at)
        {
            _player = player;
            _at = at;
        }
        
        public Vector2 GetDirection()
        {
            return (_player.position - _at.position).normalized;
        }
    }
}