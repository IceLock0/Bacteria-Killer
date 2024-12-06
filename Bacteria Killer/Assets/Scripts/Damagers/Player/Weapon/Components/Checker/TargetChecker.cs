using Entities.Player;
using UnityEngine;

namespace Damagers.Player.Weapon
{
    public class TargetChecker: WeaponChecker
    {
        private readonly PlayerClosestEnemyDetector _enemyDetector;
        
        public TargetChecker(PlayerClosestEnemyDetector enemyDetector)
        {
            _enemyDetector = enemyDetector;
        }
        
        public override void Check()
        {
            if(_enemyDetector.ClosestEnemy != null)
                base.Check();
        }
    }
}