using System;
using UnityEngine;

namespace Movement
{
    public class Mover : MonoBehaviour
    {
        private IMoveable _moveable;

        private void Awake()
        {
            _moveable = GetComponent<IMoveable>();

            if (_moveable == null)
                throw new NullReferenceException("IMoveable component not found.");
        }

        private void FixedUpdate()
        {
            _moveable.Move(_moveable.DirectionProvider.GetDirection() * Time.fixedDeltaTime);
        }
    }
}