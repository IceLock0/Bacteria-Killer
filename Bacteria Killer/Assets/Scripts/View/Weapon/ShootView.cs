using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Entities.Player;
using UnityEngine;
using Weapon;

namespace View.Weapon
{
    public class ShootView : IDisposable
    {
        private readonly WeaponShootSystem _weaponWeaponShootSystem;
        private readonly LineRenderer _shootPrefab;

        private readonly PlayerClosestEnemyDetector _playerClosestEnemyDetector;

        private readonly float _duration;
        
        private Transform _firePoint, _target;
        
        public ShootView(LineRenderer shootPrefab, WeaponShootSystem weaponShootSystem, Transform firePoint, PlayerClosestEnemyDetector playerClosestEnemyDetector, float duration)
        {
            _shootPrefab = shootPrefab;

            _weaponWeaponShootSystem = weaponShootSystem;
            _weaponWeaponShootSystem.TargetShooted += ShowShoot;

            _firePoint = firePoint;

            _playerClosestEnemyDetector = playerClosestEnemyDetector;

            _duration = duration;
        }

        public void Dispose()
        {
            _weaponWeaponShootSystem.TargetShooted -= ShowShoot;
        }
        
        private void ShowShoot()
        {
            CreateShoot().Forget();
        }

        private async UniTaskVoid CreateShoot()
        {
            _target = _playerClosestEnemyDetector.ClosestEnemy.transform;

            Vector3[] positions = new[] {_firePoint.position, _target.position};

            _shootPrefab.positionCount = positions.Length;
            
            _shootPrefab.SetPositions(positions);

            _shootPrefab.enabled = true;
            
            await Task.Delay((int)(_duration * 1000));
                
            _shootPrefab.enabled = false;
        }
    }
}