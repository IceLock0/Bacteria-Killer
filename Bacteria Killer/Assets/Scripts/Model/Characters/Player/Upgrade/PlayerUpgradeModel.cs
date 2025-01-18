using System;
using Configs.Upgrade;
using Enums.Upgrade;

namespace Model.Characters.Player.Upgrade
{
    public class PlayerUpgradeModel
    {
        private readonly UpgradeType _upgradeType;
        
        private readonly int _maxUpgrade;
        private int _currentUpgrade;

        private float _upgradeValue;
        
        public PlayerUpgradeModel(UpgradeType upgradeType, UpgradeConfig upgradeConfig)
        {
            _upgradeType = upgradeType;
            
            _maxUpgrade = upgradeConfig.MaxUpgrade;
            _currentUpgrade = 0;

            _upgradeValue = upgradeConfig.UpgradeValue;
        }

        public event Action<UpgradeType, float> Upgraded;

        public void Upgrade()
        {
            if (IsUpgradedMax())
                return;

            _currentUpgrade++;
            
            Upgraded?.Invoke(_upgradeType, _upgradeValue);
        }

        public bool IsUpgradedMax()
        {
            return _currentUpgrade >= _maxUpgrade;
        }
    }
}