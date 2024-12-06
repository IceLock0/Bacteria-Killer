using Configs.Entities;
using UnityEngine;

namespace Movement
{
    public class Mover
    {
        private readonly IDirectionProvider _directionProvider;

        private readonly EntityConfig _config;

        private readonly Rigidbody2D _rb;

        public Mover(IDirectionProvider directionProvider, EntityConfig entityConfig, Rigidbody2D rb)
        {
            _directionProvider = directionProvider;

            _config = entityConfig;

            _rb = rb;
            _rb.freezeRotation = true;
            _rb.gravityScale = 0.0f;
        }

        public void Move(float deltaTime)
        {
            _rb.velocity = _directionProvider.GetDirection() * _config.LinearSpeed * deltaTime;
        }
    }
}