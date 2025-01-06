using Services.Movement.DirectionProvider;
using UnityEngine;

namespace Services.Movement.Mover
{
    public class MoverService : IMoverService
    {
        private readonly IDirectionProviderService _directionProviderService;
        
        private readonly Rigidbody2D _rb;

        public MoverService(IDirectionProviderService directionProviderService, Rigidbody2D rb)
        {
            _directionProviderService = directionProviderService;

            _rb = rb;
        }

        public void Move(float deltaTime, float speed)
        {
            _rb.velocity = _directionProviderService.GetDirection() * speed * deltaTime;
        }
    }
}