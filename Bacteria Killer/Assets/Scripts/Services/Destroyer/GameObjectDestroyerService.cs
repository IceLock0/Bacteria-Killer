using UnityEngine;

namespace Services.Destroyer
{
    public class GameObjectDestroyerService : MonoBehaviour, IGameObjectDestroyerService
    {
        public void Destroy(GameObject gameObject)
        {
            GameObject.Destroy(gameObject);
        }
    }
}