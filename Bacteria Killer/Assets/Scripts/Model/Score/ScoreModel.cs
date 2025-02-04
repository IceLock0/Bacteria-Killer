using System;
using Data;
using Utils.XpCalculator;

namespace Model.Score
{
    public class ScoreModel
    {
        private readonly float _baseScore = 1;

        private float _currentScore;

        public event Action<float> Increased;

        public void IncreaseScore(float difficult)
        {
            var score = XpAndScoreCalculator.CalculateScoreForEnemy(_baseScore, difficult);

            _currentScore += score;

            Increased?.Invoke(_currentScore);
        }

        public bool IsPrevScoreGreaterThanCurrentScore(float prevScore)
        {
            return prevScore > _currentScore;
        }
        
        public ScoreData CreateScoreData()
        {
           return new ScoreData() { Score = _currentScore };
        }
    }
}