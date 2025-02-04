using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Model.Weapon;
using UnityEngine;

namespace Damagers.Player.Weapon
{
    public class ReloadChecker : WeaponChecker
    {
        private readonly WeaponPresenter _weaponPresenter;

        private bool _isReloading = false;
        
        public ReloadChecker(WeaponPresenter weaponPresenter)
        {
            _weaponPresenter = weaponPresenter;
        }

        public override void Check()
        {
            if (!_weaponPresenter.IsEnoughAmmoToShoot())
            {
                if (_isReloading == false)
                    Reload().Forget();
            }
            else
            {
                base.Check();
            }
        }

        private async UniTaskVoid Reload()
        { 
            _isReloading = true;
            await _weaponPresenter.GetReloadTask();
            _weaponPresenter.Reload();
            _isReloading = false;
        }
    }
}