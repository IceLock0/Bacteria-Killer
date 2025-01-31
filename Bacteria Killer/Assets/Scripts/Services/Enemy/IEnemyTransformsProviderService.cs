using System;
using UnityEngine;

namespace Services.Enemy
{
    public interface IEnemyTransformsProviderService
    {
        public void AddTransform(Transform transform);
        public void RemoveTransform(Transform transform);
        
        public event Action<Transform> Added;
        public event Action<Transform> Removed;
    }
}