using System.IO;
using UnityEngine;
using Utils.ResourcesPathes.UI;
using Zenject;

namespace Utils.Factory.UI
{
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _container;

        private Canvas _mainCanvas;
        private Camera _mainCamera;
        
        public UIFactory(DiContainer container)
        {
            _container = container;
            
            Load();
            
            _mainCamera = Camera.main;
        }

        public Canvas CreateCanvas()
        {
            var createdCanvas = _container.InstantiatePrefabForComponent<Canvas>(_mainCanvas);

            createdCanvas.worldCamera = _mainCamera;

            return createdCanvas;
        }
        
        private void Load()
        {
            _mainCanvas = Resources.Load<Canvas>(UIResourcesPathProvider.MAINCANVAS);
        }
    }
}