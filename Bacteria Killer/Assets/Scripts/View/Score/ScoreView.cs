using Presenter.Score;
using Services.Destroyer;
using TMPro;
using UnityEngine;
using Zenject;

namespace View.Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        private ScorePresenter _scorePresenter;
        
        [Inject]
        public void Initialize(IGameObjectDestroyerService gameObjectDestroyerService)
        {
            _scorePresenter = new ScorePresenter(this, gameObjectDestroyerService);
            SetScore(0);
        }

        public void SetScore(float value)
        {
            _scoreText.text = value.ToString("0.00",
                System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        }
        
        private void OnEnable()
        {
            _scorePresenter.OnEnable();
        }

        private void OnDisable()
        {
            _scorePresenter.OnDisable();
        }
    }
}