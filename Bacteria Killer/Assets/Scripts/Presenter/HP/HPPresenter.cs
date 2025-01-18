using System;
using Configs.Entities;
using Model.HP;
using Services.Upgrade;
using View.HP;

namespace Presenter.HP
{
    public class HPPresenter
    {
        private readonly HPModel _model;
        private readonly HPView _view;

        private readonly IPlayerUpgradeProviderService _playerUpgradeProviderService;

        private readonly bool _isPlayer;
        
        public HPPresenter(HPView view, CharacterConfig characterConfig, IPlayerUpgradeProviderService playerUpgradeProviderService, bool isPlayer = false)
        {
            _view = view;
            _model = new HPModel(characterConfig.MaxHp);

            _playerUpgradeProviderService = playerUpgradeProviderService;

            _isPlayer = isPlayer;
        }

        public event Action Destroyed;

        public void OnEnable()
        {
            _model.Changed += _view.UpdateImage;
            _model.Died += DestroyEntity;

            _playerUpgradeProviderService.MaxHpUpgraded += IncreaseMaxHp;
        }

        public void OnDisable()
        {
            _model.Changed -= _view.UpdateImage;
            _model.Died -= DestroyEntity;
            
            _playerUpgradeProviderService.MaxHpUpgraded -= IncreaseMaxHp;
        }

        public void TakeDamage(float value)
        {
            _model.TakeDamage(value);
        }

        public void Heal(float value)
        {
            _model.Heal(value);
        }

        private void IncreaseMaxHp(float value)
        {
            if(_isPlayer)
                _model.IncreaseMaxHp(value);
        }

        private void DestroyEntity()
        {
            _view.Destroy();
            Destroyed?.Invoke();
        }
    }
}