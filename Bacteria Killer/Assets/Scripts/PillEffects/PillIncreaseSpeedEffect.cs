using Cysharp.Threading.Tasks;
using Presenter.Character;
using UnityEngine;
using View.Characters;

namespace PillEffects
{
    public class PillIncreaseSpeedEffect : IPillEffect
    {
        private readonly float _increaseValue;
        private readonly int _durationMs;

        public PillIncreaseSpeedEffect(float increaseValue, float durationSec)
        {
            _increaseValue = increaseValue;
            _durationMs = (int)(durationSec * 1000);
        }
        
        public void Apply(Collider2D collider)
        {
            CharacterPresenter characterPresenter = collider.GetComponent<CharacterView>().Presenter;

            IncreaseSpeed(characterPresenter).Forget();
        }

        private async UniTaskVoid IncreaseSpeed(CharacterPresenter characterPresenter)
        {
            characterPresenter.IncreaseSpeed(_increaseValue);
            await UniTask.Delay(_durationMs);
            characterPresenter.DecreaseSpeed(_increaseValue);
        }
    }
}