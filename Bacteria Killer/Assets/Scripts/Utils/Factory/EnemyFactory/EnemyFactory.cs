using Configs.Entities;
using UnityEngine;
using Utils.ResourcesPathes.Configs;
using Utils.ResourcesPathes.Enemy;
using View;
using Zenject;

namespace Services.Fabric.EnemyFabric
{
    public class EnemyFactory : IEnemyFactory
    {
        private EnemyView[] _enemies;
        
        private EnemyConfig[] _configs;
        
        private DiContainer _container;

        public EnemyFactory(DiContainer container)
        {
            _container = container;
            Load();
        }

        public EnemyView Create(float difficult, Vector2 at, Quaternion rotation,  out EnemyConfig resultConfig, Transform parent = null)
        {
            var config = GetConfig(difficult);

            if (config == null)
            {
                resultConfig = null;
                return null;
            }

            var prefab = GetRandomPrefab();
            prefab.gameObject.SetActive(false);
            
            EnemyView enemy = _container.InstantiatePrefabForComponent<EnemyView>(prefab, at, rotation, parent);
            
            prefab.gameObject.SetActive(true);
            
            enemy.InitializeByFabric(config);
            
            enemy.gameObject.SetActive(true);
            
            resultConfig = config;
            return enemy;
        }

        private EnemyConfig GetConfig(float difficult)
        {
            EnemyConfig enemyConfig = null;
            
            float maxDifficult = 0;
            
            foreach (var config in _configs)
            {
                if (config.Difficult > maxDifficult)
                    maxDifficult = config.Difficult;
            }

            difficult = difficult < maxDifficult ? difficult : maxDifficult;

            float closestDifficult = difficult;
            
            foreach (var config in _configs)
            {
                if (Mathf.Abs(config.Difficult - difficult) < closestDifficult)
                {
                    closestDifficult = config.Difficult;
                    enemyConfig = config;
                }
            }

            return enemyConfig;
        }
        
        private EnemyView GetRandomPrefab()
        {
            return _enemies[Random.Range(0, _enemies.Length)];
        }

        private void Load()
        {
            _enemies = Resources.LoadAll<EnemyView>(EnemyResourcesPathProvider.ENEMIES);
            _configs = Resources.LoadAll<EnemyConfig>(ConfigsResourcesPathProvider.CONFIGS_ENEMY);
        }
    }
}