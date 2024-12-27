using Services.Detector;
using UnityEngine;
using View;
using View.Characters.Enemy;

namespace Services.Target
{
    public class PlayerTargetService : ITargetService
    {
        private readonly IClosestObjectFindService _closestObjectFindService;
        private readonly Transform _playerTransform;

        private readonly Vector2 _cameraBounds;
        
        public PlayerTargetService(IClosestObjectFindService closestObjectFindService, Transform playerTransform)
        {
            _closestObjectFindService = closestObjectFindService;
            _playerTransform = playerTransform;

            _cameraBounds = GetCameraBounds();
        }

        public GameObject GetTarget()
        {
            if (_playerTransform == null)
                return null;
            
            var enemy = _closestObjectFindService.GetClosestObjectInBoxByType<EnemyView>(_playerTransform.position, _cameraBounds);
            
            return enemy == null ? null : enemy.gameObject;
        }

        private Vector2 GetCameraBounds()
        {
            var camera = Camera.main;
            
            float cameraHeight = camera.orthographicSize * 2;
            float cameraWidth = cameraHeight * camera.aspect;

            Vector2 cameraPosition = camera.transform.position;

            return new Vector2(cameraWidth, cameraHeight);
        }
    }
}