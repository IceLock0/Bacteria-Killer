using Components.Damageable;
using Presenter.HP;
using Services.Destroyer;
using Services.Movement.Mover;
using Services.Updater;
using UnityEngine;
using View.Characters;

namespace Presenter.Character
{
    public abstract class CharacterPresenter
    {
        private CharacterView _characterView;

        private readonly IUpdaterService _updaterService;

        private readonly HPPresenter _hpPresenter;

        private readonly DamageableComponent _damageableComponent;

        private IGameObjectDestroyerService _gameObjectDestroyerService;

        protected IMoverService MoverService;

        public CharacterPresenter(CharacterView characterView, IUpdaterService updaterService, HPPresenter hpPresenter,
            DamageableComponent damageableComponent, IGameObjectDestroyerService gameObjectDestroyerService)
        {
            _characterView = characterView;

            _updaterService = updaterService;

            _hpPresenter = hpPresenter;

            _damageableComponent = damageableComponent;

            _gameObjectDestroyerService = gameObjectDestroyerService;
        }

        public virtual void OnEnable()
        {
            _updaterService.FixedUpdated += FixedUpdate;
            _damageableComponent.Damaged += TakeDamage;
            _hpPresenter.Destroyed += Destroy;
        }

        public virtual void OnDisable()
        {
            _updaterService.FixedUpdated -= FixedUpdate;
            _damageableComponent.Damaged -= TakeDamage;
            _hpPresenter.Destroyed -= Destroy;
        }

        private void TakeDamage(float value)
        {
            _hpPresenter.TakeDamage(value);
        }

        private void Destroy()
        {
            //show view if needed
            _gameObjectDestroyerService.Destroy(_characterView.gameObject);
        }

        private void FixedUpdate()
        {
            MoverService.Move(Time.fixedDeltaTime);
        }
    }
}