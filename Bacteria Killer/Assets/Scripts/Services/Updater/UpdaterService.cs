using System;
using UnityEngine;

namespace Services.Updater
{
    public class UpdaterService : MonoBehaviour, IUpdaterService
    {
        public event Action Updated;
        public event Action FixedUpdated;
        
        private void Update()
        {
            Updated?.Invoke();
        }

        private void FixedUpdate()
        {
            FixedUpdated?.Invoke();
        }
    }
}