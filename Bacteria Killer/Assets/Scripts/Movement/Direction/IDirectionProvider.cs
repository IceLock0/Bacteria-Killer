using UnityEngine;

namespace Movement
{
    public interface IDirectionProvider
    {
        public Vector2 GetDirection();
    }
}