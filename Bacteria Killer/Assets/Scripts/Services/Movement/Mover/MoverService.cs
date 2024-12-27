using Services.Movement.DirectionProvider;
using UnityEngine;

namespace Services.Movement.Mover
{
    public class MoverService : IMoverService
    {
        private readonly IDirectionProviderService _directionProviderService;

        private readonly float _speed;
        
        private readonly Rigidbody2D _rb;

        public MoverService(IDirectionProviderService directionProviderService, float speed, Rigidbody2D rb)
        {
            _directionProviderService = directionProviderService;

            _speed = speed;

            _rb = rb;
        }

        public void Move(float deltaTime)
        {
            _rb.velocity = _directionProviderService.GetDirection() * _speed * deltaTime;
        }
    }
}