using Configs.Weapon.Shoot;
using UnityEngine;

namespace Configs.Weapon
{
    [CreateAssetMenu(fileName = "Weapon Config", menuName = "Configs/Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private ShootConfig _shootConfig;
        
        [SerializeField] private float fireRateSec;
        
        [SerializeField] private int _ammoCapacity;
        
        [SerializeField] private float _reloadingTimeSec;

        [SerializeField] private float _damage;

        public ShootConfig ShootConfig => _shootConfig;
        
        public float FireRateSec => fireRateSec;

        public int AmmoCapacity => _ammoCapacity;

        public float ReloadingTimeSec => _reloadingTimeSec;

        public float Damage => _damage;
    }
}