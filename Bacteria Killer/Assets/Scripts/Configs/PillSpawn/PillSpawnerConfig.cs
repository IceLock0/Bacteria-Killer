using UnityEngine;

namespace Configs.PillSpawn
{
    [CreateAssetMenu(fileName = "PillSpawner Config", menuName = "Configs/Spawners/Pill", order = 0)]
    public class PillSpawnerConfig : ScriptableObject
    {
        [SerializeField] private int _maxPills;

        [SerializeField] private float _minDistance;
        [SerializeField] private float _maxDistance;
        
        public int MaxPills => _maxPills;

        public float MinDistance => _minDistance;
        public float MaxDistance => _maxDistance;
    }
}