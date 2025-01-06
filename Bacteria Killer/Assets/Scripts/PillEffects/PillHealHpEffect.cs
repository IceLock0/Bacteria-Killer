using Presenter.HP;
using UnityEngine;
using View.HP;

namespace PillEffects
{
    public class PillHealHpEffect : IPillEffect
    {
        private readonly float _healValue;
        
        public PillHealHpEffect(float healValue)
        {
            _healValue = healValue;
        }
        
        public void Apply(Collider2D collider)
        {
            HPPresenter hpPresenter = collider.GetComponentInChildren<HPView>().Presenter;
            
            Heal(hpPresenter);
        }

        private void Heal(HPPresenter hpPresenter)
        {
            hpPresenter.Heal(_healValue);
        }
    }
}