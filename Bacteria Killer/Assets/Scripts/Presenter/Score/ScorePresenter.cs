using Data;
using Model.Score;
using Services.Destroyer;
using Services.SaveLoad;
using UnityEngine;
using Utils.SaveKeys;
using View.Characters.Enemy;
using View.Characters.Player;
using View.Score;

namespace Presenter.Score
{
    public class ScorePresenter
    {
        private readonly ScoreModel _model;
        private readonly ScoreView _view;
        
        private readonly IGameObjectDestroyerService _gameObjectDestroyerService;
        private readonly ISaveLoadService _saveLoadService;

        public ScorePresenter(ScoreView view, IGameObjectDestroyerService gameObjectDestroyerService, ISaveLoadService saveLoadService)
        {
            _model = new ScoreModel();
            _view = view;
            _gameObjectDestroyerService = gameObjectDestroyerService;
            _saveLoadService = saveLoadService;
        }
        
        public void OnEnable()
        {
            _model.Increased += _view.SetScore;
            _gameObjectDestroyerService.Destroyed += OnEnemyDestroy;
            _gameObjectDestroyerService.Destroyed += OnPlayerDestroy;
        }

        public void OnDisable()
        {
            _model.Increased -= _view.SetScore;
            _gameObjectDestroyerService.Destroyed -= OnEnemyDestroy;
            _gameObjectDestroyerService.Destroyed -= OnPlayerDestroy;
        }
        
        private void OnEnemyDestroy(GameObject destroyed)
        {
            if (!destroyed.TryGetComponent<EnemyView>(out var enemy))
                return;
            
            _model.IncreaseScore(enemy.EnemyConfig.Difficult);
        }
        
        private void OnPlayerDestroy(GameObject destroyed)
        {
            if (!destroyed.TryGetComponent<PlayerView>(out var _))
                return;

            Save();
        }

        private void Save()
        {
            var prevScore = GetPrevScore();
            
            if (_model.IsPrevScoreGreaterThanCurrentScore(prevScore))
                return;
            
            var scoreData = _model.CreateScoreData();
            _saveLoadService.Save(SaveKeys.SCORE, scoreData);
        }

        private float GetPrevScore()
        {
            var data = _saveLoadService.Load<ScoreData>(SaveKeys.SCORE);
            
            return data == null ? 0 : data.Score;
        }
    }
}