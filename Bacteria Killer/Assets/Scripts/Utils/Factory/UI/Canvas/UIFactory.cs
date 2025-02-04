using UnityEngine;
using Utils.ResourcesPathes.UI;
using View.Root;
using Zenject;

namespace Utils.Factory.UI
{
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _container;

        private UIGameplayRootView _root;
        private Camera _mainCamera;
        
        public UIFactory(DiContainer container)
        {
            _container = container;
            
            Load();
            
            _mainCamera = Camera.main;
        }

        public Canvas Create()
        {
            var createdCanvas = _container.InstantiatePrefab(_root).GetComponentInChildren<Canvas>();

            createdCanvas.worldCamera = _mainCamera;

            return createdCanvas;
        }
        
        private void Load()
        {
            _root = Resources.Load<UIGameplayRootView>(UIResourcesPathProvider.GAMEPLAY);
        }
    }
}