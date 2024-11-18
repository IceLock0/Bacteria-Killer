using UnityEngine;

namespace Movement
{
    public interface IMoveable
    {
        public IDirectionProvider DirectionProvider { get; }

        public void Move(Vector2 direction);
    }
}