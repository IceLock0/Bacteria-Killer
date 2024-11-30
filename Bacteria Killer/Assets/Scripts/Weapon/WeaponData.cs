using Configs.Weapon;

namespace Weapon
{
    public class WeaponData
    {
        public WeaponData(WeaponConfig weaponConfig)
        {
            FireRate = weaponConfig.FireRate;
            
            AmmoCapacity = weaponConfig.AmmoCapacity;
            CurrentAmmo = AmmoCapacity;
            ReloadingTime = weaponConfig.ReloadingTime;
        }

        public float FireRate { get;}

        public int CurrentAmmo { get; set; }
        public int AmmoCapacity { get; }
        
        public float ReloadingTime { get; }
    }
}