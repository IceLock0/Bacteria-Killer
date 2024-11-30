using Configs;
using Configs.Weapon;
using UnityEngine;
using Zenject;

namespace Installers.Configs
{
    public class ConfigsInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private WeaponConfig _weaponConfig;
        
        public override void InstallBindings()
        {
            BindPlayerConfig();
            BindWeaponConfig();
        }

        private void BindPlayerConfig()
        {
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
        }

        private void BindWeaponConfig()
        {
            Container.Bind<WeaponConfig>().FromInstance(_weaponConfig).AsSingle();
        }
    }
}