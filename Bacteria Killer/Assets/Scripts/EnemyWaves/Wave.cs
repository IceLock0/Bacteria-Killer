using System;
using System.Collections.Generic;
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
            _enemies.Remove(enemy);
            
            Expired?.Invoke();
        }
    }
}