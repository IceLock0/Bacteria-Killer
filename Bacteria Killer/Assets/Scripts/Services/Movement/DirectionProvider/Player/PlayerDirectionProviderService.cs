using Services.Input;
using UnityEngine;

namespace Services.Movement.DirectionProvider.Player
{
    public class PlayerDirectionProviderService : IDirectionProviderService
    {
        private readonly IInputService _inputService;

        public PlayerDirectionProviderService(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        public Vector2 GetDirection()
        {
            return _inputService.Input.Gameplay.Movement.ReadValue<Vector2>().normalized;
        }
    }
}