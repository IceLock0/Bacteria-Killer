using Configs;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerClosestEnemyDetector
    {
        private readonly ClosestObjectDetectorService _detectorService;
        private readonly Vector2 _detectorSize;
        
        public PlayerClosestEnemyDetector(ClosestObjectDetectorService detectorService, PlayerConfig playerConfig)
        {
            _detectorService = detectorService;
            _detectorSize = playerConfig.DetectorSize;
        }

        public Enemy.Enemy ClosestEnemy { get; private set; }

        public void FindClosestEnemy(Vector2 at)
        {
            ClosestEnemy = _detectorService.GetClosestObjectInBoxByType<Enemy.Enemy>(at, _detectorSize);
        }
    }
}