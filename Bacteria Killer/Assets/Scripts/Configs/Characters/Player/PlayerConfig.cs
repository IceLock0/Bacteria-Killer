using Configs.Entities;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Configs/Entities/Player", order = 0)]
    public class PlayerConfig : CharacterConfig
    {
        [SerializeField] private Vector2 _spawnPosition;
        
        public Vector2 SpawnPosition => _spawnPosition;
    }
}