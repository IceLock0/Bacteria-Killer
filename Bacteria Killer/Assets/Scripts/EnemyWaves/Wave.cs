using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using View;
using View.Characters.Enemy;

namespace EnemyWaves
{
    public class Wave
    {
        private readonly List<EnemyView> _enemies = new();

        public event Action Expired; 
        
        public void AddEnemy(EnemyView enemy)
        {
            _enemies.Add(enemy);
        }

        public void RemoveEnemy(EnemyView enemy)
        {
            if(_enemies.Contains(enemy))
                _enemies.Remove(enemy);
            else
            {
                Debug.LogWarning($"Enemy {enemy.name} not contained in enemies wave list.");
                return;
            }
            
            if(_enemies.IsEmpty())
                Expired?.Invoke();
        }
    }
}