using Configs.Weapon;

namespace Damagers.Player.Weapon
{
    public class WeaponData
    {
        public WeaponData(WeaponConfig weaponConfig)
        {
            FireRate = weaponConfig.FireRate;
            
            AmmoCapacity = weaponConfig.AmmoCapacity;
            CurrentAmmo = AmmoCapacity;
            ReloadingTime = weaponConfig.ReloadingTime;

            Damage = weaponConfig.Damage;
        }

        public float FireRate { get;}

        public int CurrentAmmo { get; set; }
        public int AmmoCapacity { get; }
        
        public float ReloadingTime { get; }

        public float Damage { get; }
    }
}