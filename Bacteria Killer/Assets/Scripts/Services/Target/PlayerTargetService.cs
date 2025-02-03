using Services.Detector;
using UnityEngine;
using View.Characters.Enemy;
using Vector2 = UnityEngine.Vector2;

namespace Services.Target
{
    public class PlayerTargetService : ITargetService
    {
        private readonly IClosestObjectFindService _closestObjectFindService;
        private readonly Transform _playerTransform;

        private readonly float _distance;

        public PlayerTargetService(IClosestObjectFindService closestObjectFindService, Transform playerTransform,
            float distance)
        {
            _closestObjectFindService = closestObjectFindService;
            _playerTransform = playerTransform;
            _distance = distance;
        }

        public GameObject GetTarget()
        {
            if (_playerTransform == null)
                return null;

            var enemy = _closestObjectFindService.GetClosestObjectInBoxByType<EnemyView>(_playerTransform.position,
                new Vector2(_distance, _distance));

            return enemy == null ? null : enemy.gameObject;
        }
    }
}