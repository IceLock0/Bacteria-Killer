using System;
using Components.Damageable;
using Model.Weapon;
using Services.Target;
using UnityEngine;

namespace Damagers.Player.Weapon
{
    public class WeaponShootSystem
    {
        private readonly WeaponPresenter _weaponPresenter;

        private readonly ReloadChecker _reloadChecker;
        private readonly TargetChecker _targetChecker;

        private readonly WeaponChecker _sourceChecker;
        private readonly TerminalChecker _terminalChecker;

        private readonly ITargetService _targetService;

        public WeaponShootSystem(WeaponPresenter weaponPresenter, ITargetService targetService)
        {
            _weaponPresenter = weaponPresenter;
            _targetService = targetService;
            
            _reloadChecker = new ReloadChecker(_weaponPresenter);
            _targetChecker = new TargetChecker(_targetService);

            _sourceChecker = _reloadChecker;
            _terminalChecker = new TerminalChecker(_weaponPresenter);

            InitializeSequence();
        }

        public event Action<Transform> TargetShooted;

        public void OnEnable()
        {
            _terminalChecker.Shooted += OnShooted;
        }

        public void OnDisable()
        {
            _terminalChecker.Shooted -= OnShooted;
        }
        
        public void Shoot()
        {
            _sourceChecker.Check();
        }

        private void InitializeSequence()
        {
            _targetChecker.NextChecker = _terminalChecker;
            _reloadChecker.NextChecker = _targetChecker;
        }

        private void OnShooted()
        {
            var closestEnemyGo = _targetService.GetTarget();

            if(closestEnemyGo.TryGetComponent<IDamageable>(out var damageable))
                damageable.TakeDamage(_weaponPresenter.GetDamage());
            
            TargetShooted?.Invoke(closestEnemyGo.transform);
        }
    }
}