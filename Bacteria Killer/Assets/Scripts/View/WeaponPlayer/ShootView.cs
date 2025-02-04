using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace View.Weapon
{
    public class ShootView
    {
        private readonly LineRenderer _shootPrefab;
        
        private readonly Transform _firePoint;

        private readonly int _durationMs;

        private readonly CancellationToken _cancellationToken; 
        
        public ShootView(LineRenderer shootPrefab, Transform firePoint, float duration, CancellationToken cancellationToken)
        {
            _shootPrefab = shootPrefab;
            _firePoint = firePoint;

            _cancellationToken = cancellationToken;
            
            _durationMs = (int)(duration * 1000);
        }
        
        public void ShowShoot(Transform target)
        {
            if (_shootPrefab == null || target == null)
                return;
            
            CreateShoot(target, _cancellationToken).Forget();
        }

        private async UniTaskVoid CreateShoot(Transform target, CancellationToken cancellationToken)
        {
            try
            {
                Vector3[] positions = new[] { _firePoint.position, target.position };

                _shootPrefab.positionCount = positions.Length;

                _shootPrefab.SetPositions(positions);

                _shootPrefab.enabled = true;

                await UniTask.Delay(_durationMs, cancellationToken: cancellationToken);

                if(!cancellationToken.IsCancellationRequested)
                    _shootPrefab.enabled = false;
            }
            catch (OperationCanceledException)
            {
                Debug.Log("CreateShoot task was cancelled");
            }
        }
    }
}