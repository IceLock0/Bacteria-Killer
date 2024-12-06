using System;
using Enemy.Components;
using Entities.Player;

namespace Damagers.Player.Weapon
{
    public class WeaponShootSystem : IDisposable
    {
        private readonly Weapon _weapon;

        private readonly ReloadChecker _reloadChecker;
        private readonly TargetChecker _targetChecker;

        private readonly WeaponChecker _sourceChecker;

        private readonly TerminalChecker _terminalChecker;

        private readonly PlayerClosestEnemyDetector _enemyDetector;

        public WeaponShootSystem(Weapon weapon, PlayerClosestEnemyDetector enemyDetector)
        {
            _weapon = weapon;

            _reloadChecker = new ReloadChecker(weapon);
            _targetChecker = new TargetChecker(enemyDetector);

            _sourceChecker = _reloadChecker;
            _terminalChecker = new TerminalChecker(weapon);

            _enemyDetector = enemyDetector;

            InitializeSequence();
        }

        public event Action TargetShooted;

        public void Shoot()
        {
            _sourceChecker.Check();
        }

        public void Dispose()
        {
            _terminalChecker.Shooted -= OnShooted;
        }

        private void InitializeSequence()
        {
            _targetChecker.NextChecker = _terminalChecker;
            _reloadChecker.NextChecker = _targetChecker;

            _terminalChecker.Shooted += OnShooted;
        }

        private void OnShooted()
        {
            if (_enemyDetector.ClosestEnemy.gameObject.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_weapon.WeaponData.Damage);

            TargetShooted?.Invoke();
        }
    }
}