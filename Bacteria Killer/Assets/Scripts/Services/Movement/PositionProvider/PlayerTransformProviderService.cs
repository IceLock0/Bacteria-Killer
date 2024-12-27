using UnityEngine;
using View;
using View.Characters.Player;

namespace Services.Movement.PositionProvider
{
    public class PlayerTransformProviderService : IPlayerTransformProviderService
    {
        private readonly PlayerView _playerView;

        public PlayerTransformProviderService(PlayerView playerView)
        {
            _playerView = playerView;
        }

        public Transform GetTransform() =>
            _playerView.gameObject.transform;
    }
}