using System.Collections.Generic;
using Enums.Pill;
using PillEffects;
using UnityEngine;
using Utils.ResourcesPathes.Pills;
using View.Pill;
using Zenject;

namespace Utils.Factory.PillFactory
{
    public class PillFactory : IPillFactory
    {
        private readonly DiContainer _container;
        private readonly Transform _pillsContainerTransform;
        
        private PillView[] _pills;

        public PillFactory(DiContainer container)
        {
            _container = container;
            _pillsContainerTransform = new GameObject("Pills").transform;
            Load();
        }

        public PillView Create(List<IPillEffect> pillEffects, PillColorType colorType, Vector2 at, Quaternion rotation, Transform parent = null)
        {
            var prefab = GetPrefabByColorType(colorType);
            
            if (prefab == null)
            {
                Debug.LogWarning($"Pill with color type = {colorType} not founded.");
                return null;
            }

            prefab.gameObject.SetActive(false);
            var pillView = _container.InstantiatePrefabForComponent<PillView>(prefab, at, rotation, parent);
            prefab.gameObject.SetActive(true);
            
            pillView.InitializeByFabric(pillEffects);
            pillView.gameObject.SetActive(true);

            pillView.gameObject.transform.SetParent(_pillsContainerTransform);
            
            return pillView;
        }

        private PillView GetPrefabByColorType(PillColorType colorType)
        {
            foreach (PillView pill in _pills)
            {
                if (pill.Color == colorType)
                    return pill;
            }

            return null;
        }
        
        private void Load()
        {
            _pills = Resources.LoadAll<PillView>(PillResourcesPathProvider.PILLS);
        }
    }
}