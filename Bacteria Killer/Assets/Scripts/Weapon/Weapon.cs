using Configs.Weapon;
using Entities.Player;
using UnityEngine;
using Zenject;

namespace Weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private bool _isFlipped;

        private WeaponRotator _weaponRotator;

        private WeaponShootSystem _weaponShootSystem;
        
        [Inject]
        public void Initialize(WeaponConfig weaponConfig, PlayerClosestEnemyDetector closestEnemyDetector)
        {
            WeaponData = new WeaponData(weaponConfig);
            
            _weaponRotator = new WeaponRotator(closestEnemyDetector, _isFlipped, transform);
          
            _weaponShootSystem = new WeaponShootSystem(this, closestEnemyDetector);
        }

        public WeaponData WeaponData { get; private set; }

        private void Update()
        {
            _weaponRotator.Rotate();
            _weaponShootSystem.Shoot();
        }
    }
    
}