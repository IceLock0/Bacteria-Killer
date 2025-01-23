using UnityEngine;

namespace Configs.Level
{
    [CreateAssetMenu(fileName = "EnemyXp Config", menuName = "Configs/Entities/Player/Level/EnemyXp", order = 0)]
    public class EnemyXpConfig : ScriptableObject
    {
        [SerializeField] private float _baseXp;
        [SerializeField] private float _scaleValue;
        
        public float BaseXp => _baseXp;
        public float ScaleValue => _scaleValue;
    }
}