using UnityEngine;

namespace Services.Destroyer
{
    public interface IGameObjectDestroyerService
    {
        public void Destroy(GameObject gameObject);
    }
}