using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Weapon
{
    public class TerminalChecker : WeaponChecker
    {
        private readonly Weapon _weapon;

        private bool _isCooldownExpired = true;

        public TerminalChecker(Weapon weapon)
        {
            _weapon = weapon;
        }

        public override void Check()
        {
            if (_isCooldownExpired)
            {
                Debug.Log("Shoot");
                _weapon.WeaponData.CurrentAmmo--;
                _isCooldownExpired = false;
                WaitNextFire().Forget();
            }
        }

        private async UniTaskVoid WaitNextFire()
        {
            await Task.Delay((int) (_weapon.WeaponData.FireRate * 1000));
            _isCooldownExpired = true;
        }
    }
}