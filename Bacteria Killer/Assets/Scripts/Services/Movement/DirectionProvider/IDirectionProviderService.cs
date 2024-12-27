using UnityEngine;

namespace Services.Movement.DirectionProvider
{
    public interface IDirectionProviderService
    {
        public Vector2 GetDirection();
    }
}