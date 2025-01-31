using System.Collections.Generic;
using System.Linq;
using Configs;
using Configs.PillEffects;
using Configs.PillSpawn;
using Configs.Wave;
using EnemyWaves;
using Services.Destroyer;
using Services.Enemy;
using Services.Fabric.EnemyFabric;
using Services.Fabric.PlayerFabric;
using Services.Upgrade;
using UnityEngine;
using UnityEngine.UI;
using Utils.Factory.PillFactory;
using Utils.Factory.UI;
using View.Characters.Player;
using View.Characters.Player.Level;
using View.Characters.Player.Upgrade;
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

        private IUIFactory _uiFactory;
        private IUpgradeViewFactory _upgradeViewFactory;
        private Canvas _rootCanvas;
        private List<PlayerUpgradeView> _upgradeViews;

        private IPlayerUpgradeProviderService _playerUpgradeProviderService;
        
        private IEnemyTransformsProviderService _enemyTransformsProviderService;

        [Inject]
        public void Initialize(IPlayerFactory playerFactory, PlayerConfig playerConfig, IEnemyFactory enemyFactory,
            WaveConfig waveConfig, IGameObjectDestroyerService gameObjectDestroyerService, IPillFactory pillFactory,
            PillSpawnerConfig pillSpawnerConfig, PillEffectsConfig pillEffectsConfig, IUIFactory uiFactory,
            IUpgradeViewFactory upgradeViewFactory, IPlayerUpgradeProviderService playerUpgradeProviderService,
            IEnemyTransformsProviderService enemyTransformsProviderService)
        {
            _enemyTransformsProviderService = enemyTransformsProviderService;
            _playerFactory = playerFactory;
            _playerConfig = playerConfig;

            _enemyFactory = enemyFactory;
            _waveConfig = waveConfig;

            _gameObjectDestroyerService = gameObjectDestroyerService;

            _pillFactory = pillFactory;
            _pillSpawnerConfig = pillSpawnerConfig;
            _pillEffectsConfig = pillEffectsConfig;

            _uiFactory = uiFactory;
            _upgradeViewFactory = upgradeViewFactory;

            _playerUpgradeProviderService = playerUpgradeProviderService;

            _enemyTransformsProviderService = enemyTransformsProviderService;
        }

        private void Awake()
        {
            CreatePlayer();
            
            CreateUI();

            CreateUpgradeViewsAndInitUpgradeService();

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

            foreach (PlayerUpgradeView playerUpgradeView in _upgradeViews)
            {
                _playerUpgradeProviderService.UnregisterPresenter(playerUpgradeView.PlayerUpgradePresenter);
            }
        }

        private void CreateUI()
        {
            _rootCanvas = _uiFactory.Create();
        }

        private void CreateUpgradeViewsAndInitUpgradeService()
        {
            var playerLevelRoot = _rootCanvas.GetComponentInChildren<PlayerLevelView>();
            var upgradesRoot = playerLevelRoot.GetComponentInChildren<VerticalLayoutGroup>();

            _upgradeViews = _upgradeViewFactory.CreateUpgradeViews(upgradesRoot.transform);

            foreach (PlayerUpgradeView playerUpgradeView in _upgradeViews)
            {
                _playerUpgradeProviderService.RegisterPresenter(playerUpgradeView.PlayerUpgradePresenter);
            }
        }

        private void CreatePlayer()
        {
            _playerView = _playerFactory.Create(_playerConfig.SpawnPosition, Quaternion.identity);
        }


        private void CreateWaveHandler()
        {
            _waveHandler = new WaveHandler(_gameObjectDestroyerService, _enemyFactory, _waveConfig,
                _playerView.transform, _enemyTransformsProviderService);
        }

        private void CreatePillSpawner()
        {
            _pillSpawner = new PillSpawner.PillSpawner(_pillFactory, _pillSpawnerConfig, _gameObjectDestroyerService,
                _playerView.transform, _pillEffectsConfig);
        }
    }
}