using UnityEngine;

namespace Configs.Level
{
    [CreateAssetMenu(fileName = "PlayerLevel Config", menuName = "Configs/Entities/Player/Level", order = 0)]
    public class PlayerLevelConfig : ScriptableObject
    {
        [SerializeField] private int _maxLevel;
        [SerializeField] private float _xpFactorPercent;
        [SerializeField] private float _startXpTarget;
        
        public int MaxLevel => _maxLevel;
        public float XpFactorPercent => _xpFactorPercent;
        public float StartXpTarget => _startXpTarget;

    }
}