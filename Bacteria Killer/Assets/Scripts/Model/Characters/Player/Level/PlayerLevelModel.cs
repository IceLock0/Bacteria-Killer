using System;
using Configs.Level;

namespace Model.Characters.Player.Level
{
    public class PlayerLevelModel
    {
        private readonly int _maxLevel;
        private int _currentLevel;

        private readonly float _xpFactor;

        private float _targetXp;
        private float _currentXp;

        public PlayerLevelModel(PlayerLevelConfig playerLevelConfig)
        {
            _maxLevel = playerLevelConfig.MaxLevel;
            _xpFactor = playerLevelConfig.XpFactorPercent / 100.0f;

            _currentLevel = 0;
            _targetXp = playerLevelConfig.StartXpTarget;
            _currentXp = 0.0f;
        }

        public event Action<float, float> XpChanged;
        public event Action Upped;
        
        public void AddXp(float value)
        {
            if (value <= 0 || _currentLevel >= _maxLevel)
                return;

            _currentXp += value;

            while (IsLevelUp())
            {
                IncreaseLevel();
                
                ChangeXp();
            }
            
            XpChanged?.Invoke(_currentXp, _targetXp);
        }

        private void ChangeXp()
        {
            _currentXp -= _targetXp;
            _targetXp += _targetXp * _xpFactor;
        }
        
        private void IncreaseLevel()
        {
            if (_currentLevel >= _maxLevel)
                return;
            
            _currentLevel++;

            Upped?.Invoke();
        }

        private bool IsLevelUp()
        {
            return _currentXp >= _targetXp;
        }
    }
}