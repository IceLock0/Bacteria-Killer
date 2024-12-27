using UnityEngine;

namespace Configs.Wave
{
    [CreateAssetMenu(fileName = "Wave Config", menuName = "Configs/Wave", order = 0)]
    public class WaveConfig : ScriptableObject
    {
        [SerializeField] private float _startDifficult;
        [SerializeField] private float _difficultFactor;
        
        [SerializeField] private float _minSpawnDistance;
        [SerializeField] private float _maxSpawnDistance;
        
        public float StartDifficult => _startDifficult;
        public float DifficultFactor => _difficultFactor;
        
        public float MinSpawnDistance => _minSpawnDistance;
        public float MaxSpawnDistance => _maxSpawnDistance;
    }
}