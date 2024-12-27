using System;

namespace Services.Updater
{
    public interface IUpdaterService
    {
        public event Action Updated;
        public event Action FixedUpdated;
    }
}