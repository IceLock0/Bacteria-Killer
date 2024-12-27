using System;
using UnityEngine;

namespace Services.Destroyer
{
    public interface IGameObjectDestroyerService
    {
        public event Action<GameObject> Destroyed;
        
        public void Destroy(GameObject gameObject);
    }
}