using Configs.Weapon;

namespace Model.Weapon
{
    public class WeaponModel
    {
        public WeaponModel(WeaponConfig weaponConfig)
        {
            SetWeaponData(weaponConfig);
        }

        private void SetWeaponData(WeaponConfig weaponConfig)
        {
            FireRateMs = (int)(weaponConfig.FireRateSec * 1000);
            
            AmmoCapacity = weaponConfig.AmmoCapacity;
            
            ReloadingTimeMs = (int)(weaponConfig.ReloadingTimeSec * 1000);
            
            Damage = weaponConfig.Damage;
            
            CurrentAmmo = AmmoCapacity;
        }

        public int FireRateMs { get; private set; }
        
        public int AmmoCapacity { get; private set; }

        public int ReloadingTimeMs { get; private set; }
        
        public float Damage { get; private set; }
        
        public int CurrentAmmo { get; set; }

        public bool IsEnoughAmmoToShoot()
            => CurrentAmmo >= 0;

        public void Reload()
            => CurrentAmmo = AmmoCapacity;

        public void Shoot()
            => CurrentAmmo--;
    }
}