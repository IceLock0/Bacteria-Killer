using UnityEngine;

namespace Utils.XpCalculator
{
    public struct XpAndScoreCalculator
    {
        public static float CalculateTargetLevelXp(float baseXp, float scaleValue, int currentLevel)
        {
            return baseXp * Mathf.Pow(scaleValue, currentLevel - 1);
        }

        public static float CalculateXpForEnemy(float baseXp, float scaleValue, float difficult)
        {
            return baseXp * Mathf.Pow(scaleValue, difficult);
        }

        public static float CalculateScoreForEnemy(float baseScore, float difficult)
        {
            return baseScore * difficult;
        }
    }
}