using System;
using Configs.Level;
using Utils.XpCalculator;

namespace Model.Characters.Player.Level
{
    public class PlayerLevelModel
    {
        private readonly int _maxLevel;
        private int _currentLevel;

        private readonly float _scaleValue;

        private readonly float _baseXp;
        private float _targetXp;
        private float _currentXp;

        public PlayerLevelModel(PlayerLevelConfig playerLevelConfig)
        {
            _maxLevel = playerLevelConfig.MaxLevel;
            _scaleValue = playerLevelConfig.ScaleValue;

            _currentLevel = 0;
            _baseXp = playerLevelConfig.BaseXp;
            _targetXp = _baseXp;
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

        private bool IsLevelUp()
        {
            return _currentXp >= _targetXp;
        }
        
        private void IncreaseLevel()
        {
            if (_currentLevel >= _maxLevel)
                return;
            
            _currentLevel++;

            Upped?.Invoke();
        }
        
        private void ChangeXp()
        {
            _currentXp -= _targetXp;
            _targetXp = GetNextTargetXp();
        }

        private float GetNextTargetXp()
        {
            return XpAndScoreCalculator.CalculateTargetLevelXp(_baseXp, _scaleValue, _currentLevel);
        }
        
    }
}