using Configs;
using Configs.PillEffects;
using Configs.PillSpawn;
using Configs.Wave;
using EnemyWaves;
using Services.Destroyer;
using Services.Fabric.EnemyFabric;
using Services.Fabric.PlayerFabric;
using UnityEngine;
using Utils.Factory.PillFactory;
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

        private IPillFactory _pillFactory;
        private PillSpawner.PillSpawner _pillSpawner;
        private PillSpawnerConfig _pillSpawnerConfig;
        private PillEffectsConfig _pillEffectsConfig;

        [Inject]
        public void Initialize(IPlayerFactory playerFactory, PlayerConfig playerConfig, IEnemyFactory enemyFactory,
            WaveConfig waveConfig, IGameObjectDestroyerService gameObjectDestroyerService, IPillFactory pillFactory,
            PillSpawnerConfig pillSpawnerConfig, PillEffectsConfig pillEffectsConfig)
        {
            _playerFactory = playerFactory;
            _playerConfig = playerConfig;

            _enemyFactory = enemyFactory;
            _waveConfig = waveConfig;

            _gameObjectDestroyerService = gameObjectDestroyerService;

            _pillFactory = pillFactory;
            _pillSpawnerConfig = pillSpawnerConfig;
            _pillEffectsConfig = pillEffectsConfig;
        }

        private void Awake()
        {
            CreatePlayer();

            CreateWaveHandler();

            CreatePillSpawner();
        }

        private void OnEnable()
        {
            _waveHandler.OnEnable();
            _pillSpawner.OnEnable();
        }

        private void OnDisable()
        {
            _waveHandler.OnDisable();
            _pillSpawner.OnDisable();
        }

        private void CreatePlayer()
        {
            _playerView = _playerFactory.Create(_playerConfig.SpawnPosition, Quaternion.identity);
        }

        private void CreateWaveHandler()
        {
            _waveHandler = new WaveHandler(_gameObjectDestroyerService, _enemyFactory, _waveConfig,
                _playerView.transform);
        }

        private void CreatePillSpawner()
        {
            _pillSpawner = new PillSpawner.PillSpawner(_pillFactory, _pillSpawnerConfig, _gameObjectDestroyerService,
                _playerView.transform, _pillEffectsConfig);
        }
    }
}