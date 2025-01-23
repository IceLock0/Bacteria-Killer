using UnityEngine;

namespace Configs.Level
{
    [CreateAssetMenu(fileName = "PlayerLevel Config", menuName = "Configs/Entities/Player/Level", order = 0)]
    public class PlayerLevelConfig : ScriptableObject
    {
        [SerializeField] private int _maxLevel;
        [SerializeField] private float _scaleValue;
        [SerializeField] private float _baseXp;

        [SerializeField] private EnemyXpConfig _enemyXpConfig;
        
        public int MaxLevel => _maxLevel;
        public float ScaleValue => _scaleValue;
        public float BaseXp => _baseXp;

        public EnemyXpConfig EnemyXpConfig => _enemyXpConfig;

    }
}