using UnityEngine;

namespace Utils.RandomPosition
{
    public static class RandomPositionHandler
    {
        public static Vector2 GetRandomPosition(Vector2 at, float minDistance, float maxDistance)
        {
            var offsetX = Random.Range(-maxDistance, maxDistance);
            var offsetY = Random.Range(-maxDistance, maxDistance);
            
            while (Mathf.Abs(offsetX) < minDistance || Mathf.Abs(offsetY) < minDistance)
            {
                offsetX = Random.Range(-maxDistance, maxDistance);
                offsetY = Random.Range(-maxDistance, maxDistance);
            }

            var offsetVector2 = new Vector2(offsetX, offsetY);

            return at + offsetVector2;
        }
    }
}