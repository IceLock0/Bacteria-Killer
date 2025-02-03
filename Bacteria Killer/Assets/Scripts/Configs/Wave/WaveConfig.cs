using UnityEngine;

namespace Configs.Wave
{
    [CreateAssetMenu(fileName = "Wave Config", menuName = "Configs/Spawners/Wave", order = 0)]
    public class WaveConfig : ScriptableObject
    {
        [Header("Difficult")]
        [SerializeField] private float _startDifficult;
        [SerializeField] private float _difficultFactor;

        [Header("Boss")]
        [SerializeField] private int _bossFrequency;
        [SerializeField] private float _bossScaler;
        
        [Header("Distance")]
        [SerializeField] private float _minSpawnDistance;
        [SerializeField] private float _maxSpawnDistance;
        
        public float StartDifficult => _startDifficult;
        public float DifficultFactor => _difficultFactor;
        
        public int BossFrequency => _bossFrequency;
        public float BossScaler => _bossScaler;
        
        public float MinSpawnDistance => _minSpawnDistance;
        public float MaxSpawnDistance => _maxSpawnDistance;
    }
}