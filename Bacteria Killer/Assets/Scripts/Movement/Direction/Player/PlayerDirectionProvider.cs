﻿using UnityEngine;

namespace Movement
{
    public class PlayerDirectionProvider : IDirectionProvider
    {
        private InputService _inputService;

        public PlayerDirectionProvider(InputService inputService)
        {
            _inputService = inputService;
            _inputService.Enable();
        }
        
        public Vector2 GetDirection()
        {
            return _inputService.Gameplay.Movement.ReadValue<Vector2>();
        }
    }
}