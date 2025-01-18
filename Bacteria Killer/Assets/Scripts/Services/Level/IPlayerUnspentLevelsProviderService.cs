using System;

namespace Services.Level
{
    public interface IPlayerUnspentLevelsProviderService
    {
        public event Action LevelReceived;
        public event Action<int> LevelSpent;
        
        public void ReceiveLevel();
        public void SpendLevel();

    }
}