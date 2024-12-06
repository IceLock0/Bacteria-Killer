using Configs;
using Movement;
using Movement.Direction.Player;
using UnityEngine;
using Zenject;

namespace Entities.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        private PlayerClosestEnemyDetector _enemyDetector;
        
        private Mover _mover;
        
        [Inject]
        public void Initialize(PlayerClosestEnemyDetector closestEnemyDetector, InputService inputService, PlayerConfig config)
        {
            _enemyDetector = closestEnemyDetector;
            
            _mover = new Mover(new PlayerDirectionProvider(inputService), config, GetComponent<Rigidbody2D>());
        }

        private void Update()
        {
            _enemyDetector.FindClosestEnemy(transform.position);
        }

        private void FixedUpdate()
        {
            _mover.Move(Time.fixedDeltaTime);
        }
    }
}