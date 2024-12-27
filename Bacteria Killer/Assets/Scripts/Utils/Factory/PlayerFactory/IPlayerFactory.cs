using UnityEngine;
using View;

namespace Services.Fabric.PlayerFabric
{
    public interface IPlayerFactory
    {
        public PlayerView Create(Vector2 position, Quaternion rotation, Transform parent = null);
    }
}