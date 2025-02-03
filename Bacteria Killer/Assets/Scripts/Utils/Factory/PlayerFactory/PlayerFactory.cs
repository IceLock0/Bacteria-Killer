using UnityEngine;
using Utils.ResourcesPathes.Player;
using View;
using View.Characters.Player;
using Zenject;

namespace Services.Fabric.PlayerFabric
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly DiContainer _container;
        
        private Object _player;

        public PlayerFactory(DiContainer container)
        {
            _container = container;
            Load();
        }
        
        public PlayerView Create(Vector2 position, Quaternion rotation, Transform parent = null)
        {
            var player = _container.InstantiatePrefabForComponent<PlayerView>(_player, position, rotation, parent);
            
            var camera = Camera.main;
            
            camera?.transform.SetParent(player.gameObject.transform);
            
            return player;
        }

        private void Load()
        {
            _player = Resources.Load(PlayerResourcesPathProvider.PLAYER);
            
        }
    }
}