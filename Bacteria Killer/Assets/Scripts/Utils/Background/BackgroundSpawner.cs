using System.Collections.Generic;
using UnityEngine;
using View.Characters.Player;

namespace Utils.Background
{
    public class BackgroundSpawner : MonoBehaviour
    { 
        [SerializeField] private GameObject _backgroundPrefab;
        [SerializeField] private int _spawnRadius = 2;
        [SerializeField] private int _despawnRadius = 4;
        
        private Transform _playerTransform;
        private readonly Dictionary<Vector2, GameObject> _spawnedTilesDict = new();
        private float _backgroundWidth;
        private float _backgroundHeight;
        private Vector2 _lastPlayerTile;

        private void Start()
        {
            Initialize();
            UpdateBackground(force: true);
        }

        private void Update()
        {
            UpdateBackground();
        }

        private void Initialize()
        {
            _playerTransform = FindFirstObjectByType<PlayerView>().transform;
            
            var sprite = _backgroundPrefab.GetComponentInChildren<SpriteRenderer>();
            _backgroundWidth = sprite.bounds.size.x;
            _backgroundHeight = sprite.bounds.size.y;
        }

        private void UpdateBackground(bool force = false)
        {
            var playerTile = GetPlayerTile();

            if (force || playerTile != _lastPlayerTile)
            {
                _lastPlayerTile = playerTile;

                SpawnNewTiles(playerTile);
                RemoveOldTiles(playerTile);
            }
        }

        private Vector2 GetPlayerTile()
        {
            return new Vector2(
                Mathf.Floor(_playerTransform.position.x / _backgroundWidth) * _backgroundWidth,
                Mathf.Floor(_playerTransform.position.y / _backgroundHeight) * _backgroundHeight
            );
        }

        private void SpawnNewTiles(Vector2 playerTile)
        {
            for (var x = -_spawnRadius; x <= _spawnRadius; x++)
            {
                for (var y = -_spawnRadius; y <= _spawnRadius; y++)
                {
                    var newPos = playerTile + new Vector2(x * _backgroundWidth, y * _backgroundHeight);
                    if (!_spawnedTilesDict.ContainsKey(newPos))
                    {
                        SpawnTile(newPos);
                    }
                }
            }
        }

        private void RemoveOldTiles(Vector2 playerTile)
        {
            List<Vector2> tilesToRemove = new();
            foreach (var tilePos in _spawnedTilesDict.Keys)
            {
                if (Vector2.Distance(tilePos, playerTile) > _despawnRadius * _backgroundWidth)
                {
                    tilesToRemove.Add(tilePos);
                }
            }

            foreach (var tilePos in tilesToRemove)
            {
                Destroy(_spawnedTilesDict[tilePos]);
                _spawnedTilesDict.Remove(tilePos);
            }
        }

        private void SpawnTile(Vector2 position)
        {
            GameObject newTile = Instantiate(_backgroundPrefab, position, Quaternion.identity);
            _spawnedTilesDict[position] = newTile;
        }
    }
}
