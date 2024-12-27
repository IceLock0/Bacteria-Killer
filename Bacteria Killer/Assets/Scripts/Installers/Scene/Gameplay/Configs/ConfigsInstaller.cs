using Configs;
using Configs.Wave;
using Configs.Weapon;
using UnityEngine;
using Zenject;

namespace Installers.Configs
{
    public class ConfigsInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private WaveConfig _waveConfig;
        
        public override void InstallBindings()
        {
            BindPlayerConfig();
            BindWeaponConfig();
            BindWaveConfig();
        }

        private void BindPlayerConfig()
        {
            Container
                .Bind<PlayerConfig>()
                .FromInstance(_playerConfig)
                .AsSingle();
        }

        private void BindWeaponConfig()
        {
            Container.
                Bind<WeaponConfig>()
                .FromInstance(_weaponConfig)
                .AsSingle();
        }
        
        private void BindWaveConfig()
        {
            Container
                .Bind<WaveConfig>()
                .FromInstance(_waveConfig)
                .AsSingle();
        }
    }
}