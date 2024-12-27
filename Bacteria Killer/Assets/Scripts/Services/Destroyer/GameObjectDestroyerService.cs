using System;
using UnityEngine;

namespace Services.Destroyer
{
    public class GameObjectDestroyerService : MonoBehaviour, IGameObjectDestroyerService
    {
        public event Action<GameObject> Destroyed;

        public void Destroy(GameObject gameObject)
        {
            GameObject.Destroy(gameObject);
            Destroyed?.Invoke(gameObject);
        }
    }
}