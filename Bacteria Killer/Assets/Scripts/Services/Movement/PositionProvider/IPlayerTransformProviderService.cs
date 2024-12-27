using UnityEngine;

namespace Services.Movement.PositionProvider
{
    public interface IPlayerTransformProviderService
    {
        public Transform GetTransform();
    }
}