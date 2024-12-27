using UnityEngine;
using View;
using View.Characters.Player;

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