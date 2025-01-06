using System.Collections.Generic;
using Components.Collectable;
using Enums.Pill;
using PillEffects;
using Presenter.PillPresenter;
using Services.Destroyer;
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
        
        private CollectableComponent _collectableComponent;
        
        [Inject]
        public void Initialize(IGameObjectDestroyerService gameObjectDestroyerService)
        {
            _gameObjectDestroyerService = gameObjectDestroyerService;
            
            _collectableComponent = GetComponent<CollectableComponent>();
        }
        
        public void InitializeByFabric(List<IPillEffect> effects)
        {
            _pillPresenter = new PillPresenter(this, effects, _collectableComponent, _gameObjectDestroyerService);
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