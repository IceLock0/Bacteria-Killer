﻿using Configs;
using Configs.Wave;
using EnemyWaves;
using Services.Destroyer;
using Services.Fabric.EnemyFabric;
using Services.Fabric.PlayerFabric;
using UnityEngine;
using View;
using View.Characters.Player;
using Zenject;

namespace Bootstrap.Game
{
    public class GameBootstrap : MonoBehaviour
    {
        private IPlayerFactory _playerFactory;
        private PlayerConfig _playerConfig;

        private IEnemyFactory _enemyFactory;
        private WaveConfig _waveConfig;

        private PlayerView _playerView;

        private WaveHandler _waveHandler;

        private IGameObjectDestroyerService _gameObjectDestroyerService;
        
        [Inject]
        public void Initialize(IPlayerFactory playerFactory, PlayerConfig playerConfig, IEnemyFactory enemyFactory,
            WaveConfig waveConfig, IGameObjectDestroyerService gameObjectDestroyerService)
        {
            _playerFactory = playerFactory;
            _playerConfig = playerConfig;

            _enemyFactory = enemyFactory;
            _waveConfig = waveConfig;

            _gameObjectDestroyerService = gameObjectDestroyerService;
        }

        private void Awake()
        {
            CreatePlayer();
            
            CreateWaveHandler();
        }

        private void OnEnable()
        {
            _waveHandler.OnEnable();
        }

        private void OnDisable()
        {
            _waveHandler.OnDisable();
        }

        private void CreateWaveHandler()
        {
            _waveHandler = new WaveHandler(_gameObjectDestroyerService, _enemyFactory, _waveConfig, _playerView.transform);
        }

        private void CreatePlayer()
        {
            _playerView = _playerFactory.Create(_playerConfig.SpawnPosition, Quaternion.identity);
        }
    }
}