using System.Collections.Generic;
using Components.Collectable;
using Model.Pill;
using PillEffects;
using Services.Destroyer;
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
        
        public PillPresenter(PillView pillView, List<IPillEffect> effects, CollectableComponent collectableComponent, IGameObjectDestroyerService gameObjectDestroyerService)
        {
            _pillModel = new PillModel(effects);
            _pillView = pillView;

            _collectableComponent = collectableComponent;

            _gameObjectDestroyerService = gameObjectDestroyerService;
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
        
        public void OnEnable()
        {
            _collectableComponent.Collected += Collect;
        }

        public void OnDisable()
        {
            
            _collectableComponent.Collected -= Collect;
        }
    }
}