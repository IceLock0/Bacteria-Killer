using System.Collections.Generic;
using Components.Collectable;
using Configs.PillSpawn;
using Model.Pill;
using PillEffects;
using Services.Destroyer;
using Services.Movement.PositionProvider;
using Services.Updater;
using UnityEngine;
using View.Characters.Player;
using View.Pill;

namespace Presenter.PillPresenter
{
    public class PillPresenter
    {
        private readonly PillModel _pillModel;
        private readonly PillView _pillView;

        private readonly CollectableComponent _collectableComponent;

        private readonly IGameObjectDestroyerService _gameObjectDestroyerService;

        private readonly IUpdaterService _updaterService;

        private readonly float _aliveDistance;
        
        private readonly Transform _playerTransform;
        
        public PillPresenter(PillView pillView, List<IPillEffect> effects, CollectableComponent collectableComponent,
            IGameObjectDestroyerService gameObjectDestroyerService,
            IPlayerTransformProviderService playerTransformProviderService, IUpdaterService updaterService, float aliveDistance)
        {
            _pillModel = new PillModel(effects);
            _pillView = pillView;

            _collectableComponent = collectableComponent;

            _gameObjectDestroyerService = gameObjectDestroyerService;

            _updaterService = updaterService;

            _aliveDistance = aliveDistance;
            
            _playerTransform = playerTransformProviderService.GetTransform();
        }
        
        public void OnEnable()
        {
            _collectableComponent.Collected += Collect;
            _updaterService.Updated += Update;
        }

        public void OnDisable()
        {
            _collectableComponent.Collected -= Collect;
            _updaterService.Updated -= Update;
        }

        private void Update()
        {
            CheckDistanceToPlayer();
        }

        private void CheckDistanceToPlayer()
        {
            if (Vector2.Distance(_playerTransform.position, _pillView.transform.position) > _aliveDistance)
            {
                _gameObjectDestroyerService.Destroy(_pillView.gameObject);
            }
        }

        private void Collect(Collider2D collider)
        {
            if (!collider.TryGetComponent<PlayerView>(out _))
                return;

            foreach (IPillEffect effect in _pillModel.Effects)
            {
                effect.Apply(collider);
            }

            _gameObjectDestroyerService.Destroy(_pillView.gameObject);
        }
    }
}