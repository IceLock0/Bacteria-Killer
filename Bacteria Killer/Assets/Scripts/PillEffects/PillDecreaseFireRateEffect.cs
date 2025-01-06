using Cysharp.Threading.Tasks;
using Damagers.Player.Weapon;
using UnityEngine;
using View.Weapon;

namespace PillEffects
{
    public class PillDecreaseFireRateEffect : IPillEffect
    {
        private readonly float _decreaseValue;
        private readonly int _durationMs;

        public PillDecreaseFireRateEffect(float decreaseValue, float durationSec)
        {
            _decreaseValue = decreaseValue;
            _durationMs = (int) (durationSec * 1000);
        }
        
        public void Apply(Collider2D collider)
        {
            WeaponPresenter weaponPresenter = collider.GetComponentInChildren<WeaponView>().Presenter;
            
            IncreaseFireRate(weaponPresenter).Forget();
        }

        private async UniTaskVoid IncreaseFireRate(WeaponPresenter weaponPresenter)
        {
            weaponPresenter.DecreaseFireRate(_decreaseValue);
            await UniTask.Delay(_durationMs);
            weaponPresenter.IncreaseFireRate(_decreaseValue);
        } 
    }
}