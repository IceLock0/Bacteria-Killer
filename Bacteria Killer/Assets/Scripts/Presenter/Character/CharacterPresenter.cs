﻿using Components.Damageable;
using Model.Characters;
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
        private readonly CharacterView _characterView;

        private readonly IUpdaterService _updaterService;

        private readonly HPPresenter _hpPresenter;

        private readonly DamageableComponent _damageableComponent;

        private readonly IGameObjectDestroyerService _gameObjectDestroyerService;

        protected IMoverService MoverService;

        protected CharacterModel CharacterModel;

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

        public void IncreaseSpeed(float value)
        {
            CharacterModel.IncreaseSpeed(value);
        }

        public void DecreaseSpeed(float value)
        {
            CharacterModel.DecreaseSpeed(value);
        }

        private void TakeDamage(float value)
        {
            _hpPresenter.TakeDamage(value);
        }

        private void Destroy()
        {
            _characterView.ShowDeath();

            _gameObjectDestroyerService.Destroy(_characterView.gameObject);
        }

        private void FixedUpdate()
        {
            MoverService.Move(Time.fixedDeltaTime, CharacterModel.LinearSpeed);
        }
    }
}