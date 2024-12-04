using UnityEngine;

namespace Configs.Weapon
{
    [CreateAssetMenu(fileName = "Weapon Config", menuName = "Configs/Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private float _fireRate;
        
        [SerializeField] private int _ammoCapacity;
        
        [SerializeField] private float _reloadingTime;

        [SerializeField] private float _damage;
        
        public float FireRate => _fireRate;

        public int AmmoCapacity => _ammoCapacity;

        public float ReloadingTime => _reloadingTime;

        public float Damage => _damage;
    }
}