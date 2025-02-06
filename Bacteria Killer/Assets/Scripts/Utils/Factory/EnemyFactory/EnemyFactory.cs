using Configs.Entities;
using UnityEngine;
using Utils.ResourcesPathes.Configs;
using Utils.ResourcesPathes.Enemy;
using View.Characters.Enemy;
using Zenject;

namespace Services.Fabric.EnemyFabric
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _container;
        
        private readonly Transform _enemiesContainerTransform;
        
        private EnemyView[] _enemies;

        private EnemyConfig[] _configs;

        public EnemyFactory(DiContainer container)
        {
            _container = container;
            _enemiesContainerTransform = new GameObject("Enemies").transform;
            Load();
        }

        public EnemyView Create(float difficult, float bossScaler, Vector2 at, Quaternion rotation, out EnemyConfig resultConfig,
            Transform parent = null)
        {
            var config = GetMaxSuitableConfig(difficult);
            
            if (config == null)
            {
                resultConfig = null;
                return null;
            }

            var enemy = CreateAndInitEnemy(config, bossScaler, at, rotation, parent);

            resultConfig = config;
            return enemy;
        }

        private EnemyView CreateAndInitEnemy(EnemyConfig config, float bossScaler, Vector2 at, Quaternion rotation, Transform parent)
        {
            var prefab = GetRandomPrefab();
            prefab.gameObject.SetActive(false);

            var enemy = _container.InstantiatePrefabForComponent<EnemyView>(prefab, at, rotation, parent);

            prefab.gameObject.SetActive(true);

            enemy.InitializeByFabric(config, bossScaler);

            enemy.gameObject.SetActive(true);

            enemy.gameObject.transform.SetParent(_enemiesContainerTransform);
            
            return enemy;
        }
        
        private EnemyConfig GetMaxSuitableConfig(float difficult)
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