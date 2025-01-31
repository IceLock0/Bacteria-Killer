using System;
using Presenter.Pointer;
using Services.Destroyer;
using Services.Enemy;
using Services.Movement.PositionProvider;
using Services.Updater;
using UnityEngine;
using Zenject;

namespace View.Pointer
{
    public class PointersView : MonoBehaviour
    {
        [SerializeField] private GameObject _pointerPrefab;

        private PointersPresenter _presenter;

        [Inject]
        public void Initialize(IUpdaterService updaterService,
            IEnemyTransformsProviderService enemyTransformsProviderService,
            IPlayerTransformProviderService playerTransformProviderService,
            IGameObjectDestroyerService gameObjectDestroyerService)
        {
            _presenter = new PointersPresenter(this, _pointerPrefab, updaterService, enemyTransformsProviderService,
                playerTransformProviderService, gameObjectDestroyerService);
        }

        public void ShowPointer(Transform pointerTransform)
        {
            pointerTransform.gameObject.SetActive(true);
        }

        public void HidePointer(Transform pointerTransform)
        {
            pointerTransform.gameObject.SetActive(false);
        }
        
        private void OnEnable()
        {
            _presenter.OnEnable();
        }

        private void OnDisable()
        {
            _presenter.OnDisable();
        }
    }
}