using System.Collections.Generic;
using Enums.Camera;
using Services.Destroyer;
using Services.Enemy;
using Services.Movement.PositionProvider;
using Services.Updater;
using UnityEngine;
using View.Pointer;
using Object = UnityEngine.Object;

namespace Presenter.Pointer
{
    public class PointersPresenter
    {
        private readonly PointersView _view;

        private readonly GameObject _pointerPrefab;

        private readonly Camera _camera;

        private readonly IUpdaterService _updaterService;
        private readonly IEnemyTransformsProviderService _enemyTransformsProviderService;
        private readonly IGameObjectDestroyerService _gameObjectDestroyerService;

        private readonly Transform _playerTransform;

        private Dictionary<Transform, Transform> _pointers = new();

        public PointersPresenter(PointersView view, GameObject pointerPrefab,
            IUpdaterService updaterService,
            IEnemyTransformsProviderService enemyTransformsProviderService,
            IPlayerTransformProviderService playerTransformProviderService,
            IGameObjectDestroyerService gameObjectDestroyerService)
        {
            _view = view;

            _pointerPrefab = pointerPrefab;

            _camera = Camera.main;
            
            _updaterService = updaterService;
            _enemyTransformsProviderService = enemyTransformsProviderService;
            _gameObjectDestroyerService = gameObjectDestroyerService;
            _playerTransform = playerTransformProviderService.GetTransform();
        }

        public void OnEnable()
        {
            _updaterService.Updated += Update;
            _enemyTransformsProviderService.Added += InstantiatePointer;
            _enemyTransformsProviderService.Removed += DestroyPointer;
        }

        public void OnDisable()
        {
            _updaterService.Updated -= Update;
            _enemyTransformsProviderService.Added -= InstantiatePointer;
            _enemyTransformsProviderService.Removed -= DestroyPointer;
        }

        private void Update()
        {
            HandlePointers();
        }

        private void HandlePointers()
        {
            if (_playerTransform == null)
                return;
            
            foreach (var kvp in _pointers)
            {
                if(kvp.Key != null && kvp.Value != null)
                    SetPointerTransform(kvp.Key, kvp.Value);
            }
        }

        private void SetPointerTransform(Transform enemyTransform, Transform pointerTransform)
        {
            var position = GetPosition(enemyTransform, out var plane, pointerTransform);
            var rotation = GetRotation(plane);
            
            pointerTransform.position = position;
            pointerTransform.rotation = rotation;
        }

        private Vector3 GetPosition(Transform enemyTransform, out CameraPlanes plane, Transform pointerTransform)
        {
            var direction = enemyTransform.position - _playerTransform.position;
            var ray = new Ray(_playerTransform.position, direction);
            
            var planes = GeometryUtility.CalculateFrustumPlanes(_camera);
            
            var minDistance = Mathf.Infinity;

            plane = CameraPlanes.Right;
            
            for (int i = 0; i < planes.Length; i++)
            {
                if (planes[i].Raycast(ray, out var distance))
                {
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        plane = (CameraPlanes)i;
                    }
                }
            }
            
            CheckCloseDistance(direction.magnitude, minDistance, pointerTransform);
            
            return ray.GetPoint(minDistance);
        }

        private void CheckCloseDistance(float currentDistance, float minDistance, Transform pointerTransform)
        {
            var isClose = currentDistance < minDistance;

            if (isClose)
                _view.HidePointer(pointerTransform);
            else _view.ShowPointer(pointerTransform);
        }
        
        private Quaternion GetRotation(CameraPlanes plane)
        {
            return plane switch
            {
                CameraPlanes.Left => Quaternion.Euler(0, 0, 180),
                CameraPlanes.Right => Quaternion.Euler(0, 0, 0),
                CameraPlanes.Up => Quaternion.Euler(0, 0, 90),
                CameraPlanes.Down => Quaternion.Euler(0, 0, -90),
                _ => Quaternion.identity
            };
        }

        private void InstantiatePointer(Transform enemyTransform)
        {
            if (_pointers.ContainsKey(enemyTransform))
                return;

            var pointerTransform = Object.Instantiate(_pointerPrefab, _view.transform).GetComponent<Transform>();

            _pointers[enemyTransform] = pointerTransform;
        }
        
        private void DestroyPointer(Transform enemyTransform)
        {
            if (!_pointers.TryGetValue(enemyTransform, out var pointerTransform))
                return;

            _pointers.Remove(enemyTransform);
            
            _gameObjectDestroyerService.Destroy(pointerTransform.gameObject);
        }
    }
}