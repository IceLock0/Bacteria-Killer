using UnityEngine;

namespace Services.Detector
{
    public interface IClosestObjectFindService
    {
        public T GetClosestObjectInBoxByType<T>(Vector2 at, Vector2 size, float angle = 0);
        public T GetClosestObjectInCircleByType<T>(Vector2 at, float radius);
    }
}