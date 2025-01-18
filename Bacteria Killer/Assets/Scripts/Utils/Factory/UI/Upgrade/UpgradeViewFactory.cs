using System.Collections.Generic;
using System.Linq;
using Configs.Upgrade;
using Enums.Upgrade;
using Services.Upgrade;
using UnityEngine;
using Utils.ResourcesPathes.Configs;
using Utils.ResourcesPathes.UI;
using View.Characters.Player.Upgrade;
using Zenject;

namespace Utils.Factory.UI
{
    public class UpgradeViewFactory : IUpgradeViewFactory
    {
        private readonly DiContainer _container;

        private PlayerUpgradeView[] _upgradeViews;
        private UpgradeConfig[] _upgradeConfigs;

        public UpgradeViewFactory(DiContainer container)
        {
            _container = container;

            Load();
        }

        public List<PlayerUpgradeView> CreateUpgradeViews(Transform root)
        {
            List<PlayerUpgradeView> resultUpgradeViews = new();

            foreach (PlayerUpgradeView playerUpgradeView in _upgradeViews)
            {
                PlayerUpgradeView upgradeView = CreateUpgradeView(playerUpgradeView, root);
                resultUpgradeViews.Add(upgradeView);
            }

            return resultUpgradeViews;
        }

        private PlayerUpgradeView CreateUpgradeView(PlayerUpgradeView prefab, Transform root)
        {
            prefab.gameObject.SetActive(false);

            var upgradeView = _container.InstantiatePrefabForComponent<PlayerUpgradeView>(prefab, root);

            UpgradeConfig config = GetConfig(upgradeView.UpgradeType);

            upgradeView.InitializeByFabric(config);

            prefab.gameObject.SetActive(true);
            upgradeView.gameObject.SetActive(true);

            return upgradeView;
        }

        private UpgradeConfig GetConfig(UpgradeType upgradeType)
        {
            return _upgradeConfigs.FirstOrDefault(upgradeConfig => upgradeConfig.UpgradeType == upgradeType);
        }

        private void Load()
        {
            _upgradeViews = Resources.LoadAll<PlayerUpgradeView>(UIResourcesPathProvider.UPGRADES);
            _upgradeConfigs = Resources.LoadAll<UpgradeConfig>(ConfigsResourcesPathProvider.CONFIGS_UPGRADES);
        }
    }
}