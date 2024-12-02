using System;
using Entities.Player;

namespace Weapon
{
    public class WeaponShootSystem : IDisposable
    {
        private readonly Weapon _weapon;
        
        private readonly ReloadChecker _reloadChecker;
        private readonly TargetChecker _targetChecker;

        private readonly WeaponChecker _sourceChecker;
        
        private readonly TerminalChecker _terminalChecker;

        public WeaponShootSystem(Weapon weapon, PlayerClosestEnemyDetector enemyDetector)
        {
            _weapon = weapon;
            
            _reloadChecker = new ReloadChecker(weapon);
            _targetChecker = new TargetChecker(enemyDetector);

            _sourceChecker = _reloadChecker;
            _terminalChecker = new TerminalChecker(weapon);
            
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
            TargetShooted?.Invoke();
        }
        
    }
}