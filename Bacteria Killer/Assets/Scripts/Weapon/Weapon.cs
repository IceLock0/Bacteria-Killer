using Configs.Weapon;
using Entities.Player;
using UnityEngine;
using View.Weapon;
using Zenject;

namespace Weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private bool _isFlipped;

        [SerializeField] private Transform _firePoint;
        
        [SerializeField] private LineRenderer _shootPrefab;
        
        private WeaponRotator _weaponRotator;

        private WeaponShootSystem _weaponShootSystem;

        private ShootView _shootView;
        
        [Inject]
        public void Initialize(WeaponConfig weaponConfig, PlayerClosestEnemyDetector closestEnemyDetector)
        {
            WeaponData = new WeaponData(weaponConfig);
            
            _weaponRotator = new WeaponRotator(closestEnemyDetector, _isFlipped, transform);
          
            _weaponShootSystem = new WeaponShootSystem(this, closestEnemyDetector);

            _shootPrefab = Instantiate(_shootPrefab);
            
            _shootView = new ShootView(_shootPrefab, _weaponShootSystem, _firePoint, closestEnemyDetector, weaponConfig.FireRate * 0.1f);

            
        }

        public WeaponData WeaponData { get; private set; }

        private void Update()
        {
            _weaponRotator.Rotate();
            _weaponShootSystem.Shoot();
        }
    }
    
}