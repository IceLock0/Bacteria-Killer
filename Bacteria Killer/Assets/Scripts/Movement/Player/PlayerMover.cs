using Configs;
using UnityEngine;
using UnityEngineInternal;
using Zenject;

namespace Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour, IMoveable
    {
        public IDirectionProvider DirectionProvider { get; private set; }

        private Rigidbody2D _rigidbody;

        private PlayerConfig _playerConfig;
        
        [Inject]
        public void Initialize(InputService inputService, PlayerConfig playerConfig)
        {
            DirectionProvider = new PlayerDirectionProvider(inputService);

            _playerConfig = playerConfig;
            
            _rigidbody = GetComponent<Rigidbody2D>();

            _rigidbody.gravityScale = 0.0f;
            _rigidbody.freezeRotation = true;
        }

        public void Move(Vector2 direction)
        {
            _rigidbody.velocity = direction * _playerConfig.LinearSpeed;
        }
    }
}