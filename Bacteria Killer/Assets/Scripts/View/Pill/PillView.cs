using System.Collections.Generic;
using Components.Collectable;
using Configs.PillSpawn;
using Enums.Pill;
using PillEffects;
using Presenter.PillPresenter;
using Services.Destroyer;
using Services.Movement.PositionProvider;
using Services.Updater;
using UnityEngine;
using Zenject;

namespace View.Pill
{
    public class PillView : MonoBehaviour
    {
        [SerializeField] private PillColorType _color;
        public PillColorType Color => _color;

        private PillPresenter _pillPresenter;
        
        private IGameObjectDestroyerService _gameObjectDestroyerService;

        private IPlayerTransformProviderService _playerTransformProviderService;

        private IUpdaterService _updaterService;
        
        private CollectableComponent _collectableComponent;

        private float _aliveDistance;
        
        [Inject]
        public void Initialize(IGameObjectDestroyerService gameObjectDestroyerService,
            IPlayerTransformProviderService playerTransformProviderService, IUpdaterService updaterService, PillSpawnerConfig pillSpawnerConfig)
        {
            _gameObjectDestroyerService = gameObjectDestroyerService;

            _playerTransformProviderService = playerTransformProviderService;

            _updaterService = updaterService;
            
            _aliveDistance = pillSpawnerConfig.MaxDistance;
            
            _collectableComponent = GetComponent<CollectableComponent>();
        }
        
        public void InitializeByFabric(List<IPillEffect> effects)
        {
            _pillPresenter = new PillPresenter(this, effects, _collectableComponent, _gameObjectDestroyerService, _playerTransformProviderService, _updaterService, _aliveDistance);
        }

        private void OnEnable()
        {
            _pillPresenter.OnEnable();
        }

        private void OnDisable()
        {
            _pillPresenter.OnDisable();
        }
    }
}