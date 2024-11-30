using Entities.Player;

namespace Weapon
{
    public class WeaponShootSystem
    {
        private readonly Weapon _weapon;
        
        private readonly ReloadChecker _reloadChecker;
        private readonly TargetChecker _targetChecker;

        private readonly WeaponChecker _sourceChecker;
        private readonly WeaponChecker _terminalChecker;

        public WeaponShootSystem(Weapon weapon, PlayerClosestEnemyDetector enemyDetector)
        {
            _weapon = weapon;
            
            _reloadChecker = new ReloadChecker(weapon);
            _targetChecker = new TargetChecker(enemyDetector);

            _sourceChecker = _reloadChecker;
            _terminalChecker = new TerminalChecker(weapon);
            
            InitializeSequence();
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
    }
}