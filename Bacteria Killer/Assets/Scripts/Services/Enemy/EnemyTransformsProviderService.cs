using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Enemy
{
    public class EnemyTransformsProviderService : IEnemyTransformsProviderService
    {
        private readonly List<Transform> _transforms = new();
        
        public event Action<Transform> Added;
        public event Action<Transform> Removed;
        
        public void AddTransform(Transform transform)
        {
            if (_transforms.Contains(transform)) 
                return;
            
            _transforms.Add(transform);
            Added?.Invoke(transform);
        }

        public void RemoveTransform(Transform transform)
        {
            if (!_transforms.Contains(transform)) 
                return;
            
            _transforms.Remove(transform);
            Removed?.Invoke(transform);
        }
    }
}