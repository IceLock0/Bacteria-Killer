using Configs.Wave;
using Services.Destroyer;
using Services.Fabric.EnemyFabric;
using UnityEngine;
using Utils.RandomPosition;
using View.Characters.Enemy;
using Random = UnityEngine.Random;

namespace EnemyWaves
{
    public class WaveHandler
    {
        private readonly IGameObjectDestroyerService _gameObjectDestroyerService;
        
        private readonly IEnemyFactory _factory;

        private readonly Transform _playerTransform;
        
        private readonly Wave _wave;

        private readonly float _minSpawnDistance;
        private readonly float _maxSpawnDistance;
        
        private readonly float _difficultFactor;
        
        private float _nextDifficult;
        private float _usedDifficult = 0;

        public WaveHandler(IGameObjectDestroyerService gameObjectDestroyerService, IEnemyFactory factory, WaveConfig config, Transform playerTransform)
        {
            _gameObjectDestroyerService = gameObjectDestroyerService;
            
            _factory = factory;
            
            _playerTransform = playerTransform;

            _minSpawnDistance = config.MinSpawnDistance;
            _maxSpawnDistance = config.MaxSpawnDistance;

            _difficultFactor = config.DifficultFactor;
            _nextDifficult = config.StartDifficult;

            _wave = new Wave();
            
            CreateWave();
        }
        
        public void OnEnable()
        {
            _gameObjectDestroyerService.Destroyed += HandleRemoveEnemy;
            _wave.Expired += CreateWave;
        }

        public void OnDisable()
        {
            _gameObjectDestroyerService.Destroyed -= HandleRemoveEnemy;
            _wave.Expired -= CreateWave;
        }

        public void HandleRemoveEnemy(GameObject gameObject)
        {
            if(gameObject.TryGetComponent<EnemyView>(out var enemyView))
                _wave.RemoveEnemy(enemyView);
        }
        
        private void CreateWave()
        {
            Debug.Log($"WaveDifficult for create = {_nextDifficult}");
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

        private EnemyView CreateEnemy(out float resultDifficult)
        {
            var targetDifficult = GetRandomEnemyDifficult();
            var position = RandomPositionHandler.GetRandomPosition(_playerTransform.position, _minSpawnDistance, _maxSpawnDistance);
            
            EnemyView enemy = _factory.Create(targetDifficult, position, Quaternion.identity, out var resultConfig);

            resultDifficult = resultConfig == null ? 0 : resultConfig.Difficult;
            
            return enemy;
        }
    }
}