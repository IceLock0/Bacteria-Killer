using Model.Score;
using Services.Destroyer;
using UnityEngine;
using View.Characters.Enemy;
using View.Score;

namespace Presenter.Score
{
    public class ScorePresenter
    {
        private readonly ScoreModel _model;
        private readonly ScoreView _view;
        
        private readonly IGameObjectDestroyerService _gameObjectDestroyerService;

        public ScorePresenter(ScoreView view, IGameObjectDestroyerService gameObjectDestroyerService)
        {
            _model = new ScoreModel();
            _view = view;
            _gameObjectDestroyerService = gameObjectDestroyerService;
        }
        
        public void OnEnable()
        {
            _model.Increased += _view.SetScore;
            _gameObjectDestroyerService.Destroyed += OnEnemyDestroy;
        }

        public void OnDisable()
        {
            _model.Increased -= _view.SetScore;
            _gameObjectDestroyerService.Destroyed -= OnEnemyDestroy;
        }
        
        private void OnEnemyDestroy(GameObject destroyed)
        {
            if (!destroyed.TryGetComponent<EnemyView>(out var enemy))
                return;
            
            _model.IncreaseScore(enemy.EnemyConfig.Difficult);
        }
    }
}