using Configs;
using Configs.Entities;
using Configs.Weapon;
using UnityEngine;
using Zenject;

namespace Installers.Configs
{
    public class ConfigsInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private EnemyConfig _enemyConfig;
        
        public override void InstallBindings()
        {
            BindPlayerConfig();
            BindWeaponConfig();
            BindEnemyConfig();
        }

        private void BindPlayerConfig()
        {
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
        }

        private void BindWeaponConfig()
        {
            Container.Bind<WeaponConfig>().FromInstance(_weaponConfig).AsSingle();
        }

        private void BindEnemyConfig()
        {
            Container.Bind<EnemyConfig>().FromInstance(_enemyConfig).AsSingle();
        }
    }
}