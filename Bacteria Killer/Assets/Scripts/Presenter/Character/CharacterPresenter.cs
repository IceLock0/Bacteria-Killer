using Components.Damageable;
using Presenter.HP;
using Services.Movement.Mover;
using Services.Updater;
using UnityEngine;

namespace Presenter.Character
{
    public abstract class CharacterPresenter
    {
        private readonly IUpdaterService _updaterService;

        private readonly HPPresenter _hpPresenter;

        private readonly DamageableComponent _damageableComponent;
        
        protected IMoverService MoverService;
        
        public CharacterPresenter(IUpdaterService updaterService, HPPresenter hpPresenter, DamageableComponent damageableComponent)
        {
            _updaterService = updaterService;

            _hpPresenter = hpPresenter;

            _damageableComponent = damageableComponent;
        }

        public virtual void OnEnable()
        {
            _updaterService.FixedUpdated += FixedUpdate;
            _damageableComponent.Damaged += TakeDamage;
        }

        public virtual void OnDisable()
        {
            _updaterService.FixedUpdated -= FixedUpdate;
            _damageableComponent.Damaged -= TakeDamage;
        }

        private void TakeDamage(float value)
        {
            Debug.Log($"HpPresenter = {_hpPresenter == null}");
            _hpPresenter.TakeDamage(value);
        }
        
        private void FixedUpdate()
        {
            MoverService.Move(Time.fixedDeltaTime);
        }
    }
}