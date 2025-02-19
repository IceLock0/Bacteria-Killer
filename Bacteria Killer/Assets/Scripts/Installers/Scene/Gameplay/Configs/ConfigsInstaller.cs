﻿using Configs;
using Configs.Level;
using Configs.PillEffects;
using Configs.PillSpawn;
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
        [SerializeField] private PillSpawnerConfig _pillSpawnerConfig;
        [SerializeField] private PillEffectsConfig _pillEffectsConfig;
        [SerializeField] private PlayerLevelConfig _playerLevelConfig;
        
        public override void InstallBindings()
        {
            BindPlayerConfig();
            
            BindWeaponConfig();
            
            BindWaveConfig();
            
            BindPillsSpawnerConfig();
            
            BindPillEffectsConfig();
            
            BindPlayerLevelConfig();
        }
        
        private void BindPlayerLevelConfig()
        {
            Container
                .Bind<PlayerLevelConfig>()
                .FromInstance(_playerLevelConfig)
                .AsSingle();
        }

        private void BindPillEffectsConfig()
        {
            Container
                .Bind<PillEffectsConfig>()
                .FromInstance(_pillEffectsConfig)
                .AsSingle();
        }

        private void BindPillsSpawnerConfig()
        {
            Container
                .Bind<PillSpawnerConfig>()
                .FromInstance(_pillSpawnerConfig)
                .AsSingle();
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