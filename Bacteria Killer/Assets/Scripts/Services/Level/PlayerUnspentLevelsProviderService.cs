using System;
using UnityEngine;

namespace Services.Level
{
    public class PlayerUnspentLevelsProviderService : IPlayerUnspentLevelsProviderService
    {
        private int _unspentLevels = 0;
        
        public event Action<int> LevelSpent;
        public event Action LevelReceived;
        
        public void ReceiveLevel()
        {
            _unspentLevels++;
            LevelReceived?.Invoke();
        }
        
        public void SpendLevel()
        {
            _unspentLevels--;
            LevelSpent?.Invoke(_unspentLevels);
        }
    }
}