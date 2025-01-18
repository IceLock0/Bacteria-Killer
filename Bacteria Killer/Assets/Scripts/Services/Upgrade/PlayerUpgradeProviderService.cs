using System;
using System.Collections.Generic;
using Enums.Upgrade;
using Presenter.Character.Player.UpgradeBar;
using UnityEngine;

namespace Services.Upgrade
{
    public class PlayerUpgradeProviderService : IPlayerUpgradeProviderService
    {
        private readonly List<PlayerUpgradePresenter> _playerUpgradePresenters = new();
        
        public void RegisterPresenter(PlayerUpgradePresenter presenter)
        {
            if (_playerUpgradePresenters.Contains(presenter))
            {
                Debug.LogWarning($"Presenter {presenter} is already contained.");
                return;
            }

            _playerUpgradePresenters.Add(presenter);
            presenter.UpgradeCompleted += ChooseAndInvokeAction;
        }

        public void UnregisterPresenter(PlayerUpgradePresenter presenter)
        {
            if (!_playerUpgradePresenters.Contains(presenter))
            {
                Debug.LogWarning($"Presenter {presenter} not contained.");
                return;
            }

            _playerUpgradePresenters.Remove(presenter);
            presenter.UpgradeCompleted -= ChooseAndInvokeAction;
        }

        public event Action<float> DamageUpgraded;
        public event Action<float> SpeedUpgraded;
        public event Action<float> MaxHpUpgraded;

        private void ChooseAndInvokeAction(UpgradeType upgradeType, float upgradeValue)
        {
            switch (upgradeType)
            {
                case UpgradeType.Damage:
                    DamageUpgraded?.Invoke(upgradeValue);
                    break;
                case UpgradeType.Speed:
                    SpeedUpgraded?.Invoke(upgradeValue);
                    break;
                case UpgradeType.Hp:
                    MaxHpUpgraded?.Invoke(upgradeValue);
                    break;
                default:
                    Debug.LogWarning($"UpgradeType = {upgradeType}, not founded.");
                    break;
            }
        }
    }
}