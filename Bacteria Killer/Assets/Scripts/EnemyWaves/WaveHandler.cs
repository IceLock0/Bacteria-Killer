using System;
using Configs.Wave;
using Services.Fabric.EnemyFabric;
using UnityEngine;
using View;
using View.Characters.Enemy;
using Random = UnityEngine.Random;

namespace EnemyWaves
{
    public class WaveHandler
    {
        private readonly IEnemyFactory _factory;

        private readonly Transform _playerTransform;
        
        private readonly Wave _wave;

        private readonly float _minSpawnDistance;
        private readonly float _maxSpawnDistance;
        
        private readonly float _difficultFactor;
        
        private float _nextDifficult;
        private float _usedDifficult = 0;

        public WaveHandler(IEnemyFactory factory, WaveConfig config, Transform playerTransform)
        {
            _factory = factory;
            
            _playerTransform = playerTransform;

            _minSpawnDistance = config.MinSpawnDistance;
            _maxSpawnDistance = config.MaxSpawnDistance;

            _difficultFactor = config.DifficultFactor;
            _nextDifficult = config.StartDifficult;

            _wave = new Wave();
            
            CreateWave();
        }
        
        public event Action<EnemyView> EnemyDied; 
        
        public void OnEnable()
        {
            EnemyDied += _wave.RemoveEnemy;
            _wave.Expired += CreateWave;
        }

        public void OnDisable()
        {
            EnemyDied -= _wave.RemoveEnemy;
            _wave.Expired -= CreateWave;
        }
        
        public void CreateWave()
        {
            while (_usedDifficult < _nextDifficult)
            {
                AddEnemyToWave();
            }

            _usedDifficult = 0;
            _nextDifficult += _difficultFactor;
        }

        private void AddEnemyToWave()
        {
            EnemyView enemy = CreateEnemy(out var difficult);
            
            if (enemy == null)
                return;
            
            _wave.AddEnemy(enemy);

            Debug.Log("Added enemy with difficult : " + difficult);

            _usedDifficult += difficult;
        }

        private float GetRandomEnemyDifficult()
        {
            var difficult = Random.Range(0, _nextDifficult - _usedDifficult);
            return difficult;
        }

        private Vector2 GetRandomEnemyPosition()
        {
            var minDistance = _minSpawnDistance;
            var maxDistance = _maxSpawnDistance;
            
            var offsetX = Random.Range(-maxDistance, maxDistance);
            var offsetY = Random.Range(-maxDistance, maxDistance);
            
            while (Mathf.Abs(offsetX) < minDistance || Mathf.Abs(offsetY) < minDistance)
            {
                offsetX = Random.Range(-maxDistance, maxDistance);
                offsetY = Random.Range(-maxDistance, maxDistance);
            }

            var offsetVector2 = new Vector2(offsetX, offsetY);

            return (Vector2)_playerTransform.position + offsetVector2;
        }

        private EnemyView CreateEnemy(out float resultDifficult)
        {
            var targetDifficult = GetRandomEnemyDifficult();
            var position = GetRandomEnemyPosition();
            
            EnemyView enemy = _factory.Create(targetDifficult, position, Quaternion.identity, out var resultConfig);

            resultDifficult = resultConfig == null ? 0 : resultConfig.Difficult;
            
            return enemy;
        }
    }
}