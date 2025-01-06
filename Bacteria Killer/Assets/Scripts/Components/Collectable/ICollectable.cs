using System;
using UnityEngine;

namespace Components.Collectable
{
    public interface ICollectable
    {
        public event Action<Collider2D> Collected;
    }
}