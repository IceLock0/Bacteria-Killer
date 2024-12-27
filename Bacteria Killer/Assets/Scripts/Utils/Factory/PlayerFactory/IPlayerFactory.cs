using UnityEngine;
using View;
using View.Characters.Player;

namespace Services.Fabric.PlayerFabric
{
    public interface IPlayerFactory
    {
        public PlayerView Create(Vector2 position, Quaternion rotation, Transform parent = null);
    }
}