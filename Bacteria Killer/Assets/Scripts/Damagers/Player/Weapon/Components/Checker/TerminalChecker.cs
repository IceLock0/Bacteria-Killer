using System;
using Cysharp.Threading.Tasks;

namespace Damagers.Player.Weapon
{
    public class TerminalChecker : WeaponChecker
    {
        private readonly WeaponPresenter _weaponPresenter;

        private bool _isCooldownExpired = true;

        public TerminalChecker(WeaponPresenter weaponPresenter)
        {
            _weaponPresenter = weaponPresenter;
        }

        public event Action Shooted;
        
        public override void Check()
        {
            if (_isCooldownExpired)
            {
                _weaponPresenter.Shoot();
                _isCooldownExpired = false;
                WaitNextFire().Forget();
                Shooted?.Invoke();
            }
        }

        private async UniTaskVoid WaitNextFire()
        {
            await _weaponPresenter.GetFireRateTask();
            _isCooldownExpired = true;
        }
    }
}