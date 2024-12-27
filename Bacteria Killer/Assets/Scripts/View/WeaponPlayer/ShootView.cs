using Cysharp.Threading.Tasks;
using UnityEngine;

namespace View.Weapon
{
    public class ShootView
    {
        private readonly LineRenderer _shootPrefab;
        
        private readonly Transform _firePoint;

        private readonly int _durationMs;

        public ShootView(LineRenderer shootPrefab, Transform firePoint, float duration)
        {
            _shootPrefab = shootPrefab;
            _firePoint = firePoint;

            _durationMs = (int)(duration * 1000);
        }
        
        public void ShowShoot(Transform target)
        {
            CreateShoot(target).Forget();
        }

        private async UniTaskVoid CreateShoot(Transform target)
        {
            Vector3[] positions = new[] {_firePoint.position, target.position};
    
            _shootPrefab.positionCount = positions.Length;
            
            _shootPrefab.SetPositions(positions);

            _shootPrefab.enabled = true;
            
            await UniTask.Delay(_durationMs);
                
            _shootPrefab.enabled = false;
        }
    }
}