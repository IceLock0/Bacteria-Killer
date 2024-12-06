using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Damagers.Player.Weapon
{
    public class ReloadChecker : WeaponChecker
    {
        private readonly Weapon _weapon;

        private bool _isReloading = false;
        
        public ReloadChecker(Weapon weapon)
        {
            _weapon = weapon;
        }

        public override void Check()
        {
            if (_weapon.WeaponData.CurrentAmmo <= 0)
            {
                if (_isReloading == false)
                    Reload().Forget();

            }
            else
            {
                base.Check();
               // _isReloading = false;
            }
        }

        private async UniTaskVoid Reload()
        { 
            _isReloading = true;
            await Task.Delay((int)(_weapon.WeaponData.ReloadingTime * 1000));
            _weapon.WeaponData.CurrentAmmo = _weapon.WeaponData.AmmoCapacity;
            _isReloading = false;
        }
    }
}