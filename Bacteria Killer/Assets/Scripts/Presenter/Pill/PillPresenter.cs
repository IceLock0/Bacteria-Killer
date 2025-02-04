using System.Collections.Generic;
using Components.Collectable;
using Configs.PillSpawn;
using Cysharp.Threading.Tasks;
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
        private readonly PillModel _model;
        private readonly PillView _view;

        private readonly CollectableComponent _collectableComponent;

        private readonly IGameObjectDestroyerService _gameObjectDestroyerService;

        private readonly IUpdaterService _updaterService;

        private readonly float _aliveDistance;
        
        private readonly Transform _playerTransform;
        
        public PillPresenter(PillView view, List<IPillEffect> effects, CollectableComponent collectableComponent,
            IGameObjectDestroyerService gameObjectDestroyerService,
            IPlayerTransformProviderService playerTransformProviderService, IUpdaterService updaterService, float aliveDistance)
        {
            _model = new PillModel(effects);
            _view = view;

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
            if(_playerTransform != null)
                CheckDistanceToPlayer();
        }

        private void CheckDistanceToPlayer()
        {
            if (Vector2.Distance(_playerTransform.position, _view.transform.position) > _aliveDistance)
            {
                _gameObjectDestroyerService.Destroy(_view.gameObject);
            }
        }

        private void Collect(Collider2D collider)
        {
            if (!collider.TryGetComponent<PlayerView>(out _))
                return;

            foreach (var effect in _model.Effects)
                effect.Apply(collider);

            _view.ShowEffect();
            _gameObjectDestroyerService.Destroy(_view.gameObject);
        }
    }
}