using UnityEngine;

namespace Services.Detector
{
    public interface IClosestObjectFindService
    {
        public T GetClosestObjectInBoxByType<T>(Vector2 at, Vector2 size, float angle = 0);
    }
}