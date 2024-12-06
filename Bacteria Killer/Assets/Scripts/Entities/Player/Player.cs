using Configs;
using Movement.Direction.Player;
using Zenject;

namespace Entities.Player
{
    public class Player : Entity
    {
        private PlayerClosestEnemyDetector _enemyDetector;

        [Inject]
        public void Initialize(PlayerClosestEnemyDetector closestEnemyDetector, InputService inputService, PlayerConfig config)
        {
            Config = config;
            
            _enemyDetector = closestEnemyDetector;

            DirectionProvider = new PlayerDirectionProvider(inputService);
        }

        private void Update()
        {
            _enemyDetector.FindClosestEnemy(transform.position);
        }
    }
}