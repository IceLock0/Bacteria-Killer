using Configs.Wave;
using Services.Destroyer;
using Services.Enemy;
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

        private readonly int _bossFrequency;
        private int _wavesToBoss;
        private float _bossScaler;
        
        private readonly float _difficultFactor;

        private float _nextDifficult;
        private float _usedDifficult = 0;
        
        
        private readonly IEnemyTransformsProviderService _enemyTransformsProviderService;
        
        public WaveHandler(IGameObjectDestroyerService gameObjectDestroyerService, IEnemyFactory factory,
            WaveConfig config, Transform playerTransform, IEnemyTransformsProviderService enemyTransformsProviderService)
        {
            _gameObjectDestroyerService = gameObjectDestroyerService;

            _factory = factory;

            _playerTransform = playerTransform;

            _minSpawnDistance = config.MinSpawnDistance;
            _maxSpawnDistance = config.MaxSpawnDistance;

            _bossFrequency = config.BossFrequency;
            _wavesToBoss = _bossFrequency;
            _bossScaler = config.BossScaler;
            
            _difficultFactor = config.DifficultFactor;
            _nextDifficult = config.StartDifficult;

            _enemyTransformsProviderService = enemyTransformsProviderService;
            
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

        private void HandleRemoveEnemy(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<EnemyView>(out var enemyView))
            {
                _wave.RemoveEnemy(enemyView);
                _enemyTransformsProviderService.RemoveTransform(enemyView.transform);
            }
        }

        private void CreateWave()
        {
            while (_usedDifficult < _nextDifficult)
            {
                AddEnemyToWave();
            }

            _usedDifficult = 0;
            _nextDifficult += _difficultFactor;
            _wavesToBoss--;
        }

        private void AddEnemyToWave()
        {
            EnemyView enemy = CreateEnemy(out var difficult);

            if (enemy == null)
                return;

            _wave.AddEnemy(enemy);

            _enemyTransformsProviderService.AddTransform(enemy.transform);;
            
            Debug.Log("Added enemy with difficult : " + difficult);

            _usedDifficult += difficult;
        }
        
        private EnemyView CreateEnemy(out float resultDifficult)
        {
            return _wavesToBoss <= 0 ? CreateBoss(out resultDifficult) : CreateDefault(out resultDifficult);
        }

        private EnemyView CreateBoss(out float resultDifficult)
        {
            var difficult = _nextDifficult;
            var position = GetPosition();
            
            var enemy = _factory.Create(difficult, _bossScaler, position, Quaternion.identity, out var resultConfig);

            resultDifficult = 0;

            _wavesToBoss = _bossFrequency;
            
            return enemy;
        }

        private EnemyView CreateDefault(out float resultDifficult)
        {
            var difficult = GetDifficult();
            var position = GetPosition();
            
            var enemy = _factory.Create(difficult, 1, position, Quaternion.identity, out var resultConfig);

            resultDifficult = resultConfig == null ? 0 : resultConfig.Difficult;

            return enemy;
        }
        
        private float GetDifficult() =>
            Random.Range(0, _nextDifficult - _usedDifficult);
        
        private Vector2 GetPosition() =>
            RandomPositionHandler.GetRandomPosition(_playerTransform.position, _minSpawnDistance, _maxSpawnDistance);
    }
}