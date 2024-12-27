using System;

namespace Services.Input
{
    public class InputService : IInputService, IDisposable
    {
        private InputController _input;
        
        public InputController Input => _input;

        public InputService(InputController input)
        {
            _input = input;
            
            _input.Enable();
        }

        public void Dispose()
        {
            _input?.Disable();
            _input?.Dispose();
        }
    }
}