using UnityEngine;
using Zenject;

namespace Entities.Player
{
    public class Player : MonoBehaviour
    {
        private PlayerClosestEnemyDetector _enemyDetector;

        [Inject]
        public void Initialize(PlayerClosestEnemyDetector closestEnemyDetector)
        {
            _enemyDetector = closestEnemyDetector;
        }

        private void Update()
        {
            _enemyDetector.FindClosestEnemy(transform.position);
        }
    }
}