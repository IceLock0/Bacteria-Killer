using UnityEngine;
using View;

namespace Services.Target
{
    public class EnemyTargetService : ITargetService
    {
        private readonly Transform _playerTransform;
        
        public EnemyTargetService(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        public GameObject GetTarget() =>
            _playerTransform.GetComponent<PlayerView>().gameObject;
    }
}