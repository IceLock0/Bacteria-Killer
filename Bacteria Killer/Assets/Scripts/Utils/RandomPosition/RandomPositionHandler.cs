using UnityEngine;

namespace Utils.RandomPosition
{
    public static class RandomPositionHandler
    {
        public static Vector2 GetRandomPosition(Vector2 at, float minDistance, float maxDistance)
        {
            float distance = Random.Range(minDistance, maxDistance);
            float angle = Random.Range(0f, 2f * Mathf.PI);
        
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
            return at + offset;
        }
    }
}