using System;
using Presenter.Character.Player.UpgradeBar;

namespace Services.Upgrade
{
    public interface IPlayerUpgradeProviderService
    {
        public event Action<float> DamageUpgraded;
        public event Action<float> SpeedUpgraded;
        public event Action<float> MaxHpUpgraded;

        public void RegisterPresenter(PlayerUpgradePresenter presenter);
        public void UnregisterPresenter(PlayerUpgradePresenter presenter);
    }
}