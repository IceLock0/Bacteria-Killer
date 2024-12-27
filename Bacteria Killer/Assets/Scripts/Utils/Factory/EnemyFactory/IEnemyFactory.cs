using Configs.Entities;
using UnityEngine;
using View;
using View.Characters.Enemy;

namespace Services.Fabric.EnemyFabric
{
    public interface IEnemyFactory
    {
        public EnemyView Create(float difficult, Vector2 at, Quaternion rotation, out EnemyConfig resultConfig, Transform parent = null);
    }
}