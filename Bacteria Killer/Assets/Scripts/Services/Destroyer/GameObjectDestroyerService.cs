using System;
using UnityEngine;

namespace Services.Destroyer
{
    public class GameObjectDestroyerService : MonoBehaviour, IGameObjectDestroyerService
    {
        public event Action<GameObject> Destroyed;

        public void Destroy(GameObject gameObject)
        {
            Destroyed?.Invoke(gameObject);
            GameObject.Destroy(gameObject);
        }
    }
}