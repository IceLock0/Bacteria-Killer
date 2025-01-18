using Enums.Upgrade;
using UnityEngine;

namespace Configs.Upgrade
{
    [CreateAssetMenu(fileName = "Upgrade Config", menuName = "Configs/Upgrade", order = 0)]
    public class UpgradeConfig : ScriptableObject
    {
        [SerializeField] private UpgradeType _upgradeType;
        [SerializeField] private int _maxUpgrade;
        [SerializeField] private float _upgradeValue;

        public UpgradeType UpgradeType => _upgradeType;
        public int MaxUpgrade => _maxUpgrade;
        public float UpgradeValue => _upgradeValue;
    }
}