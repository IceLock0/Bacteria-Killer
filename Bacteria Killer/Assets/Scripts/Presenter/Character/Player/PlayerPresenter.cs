using Components.Damageable;
using Configs;
using Model.Characters.Player;
using Presenter.HP;
using Services.Input;
using Services.Movement.DirectionProvider;
using Services.Movement.DirectionProvider.Player;
using Services.Movement.Mover;
using Services.Updater;
using UnityEngine;

namespace Presenter.Character.Player
{
    public class PlayerPresenter
    {
        private readonly PlayerModel _playerModel;

        private readonly IUpdaterService _updaterService;

        private readonly IMoverService _moverService;
        private readonly IDirectionProviderService _directionProviderSerivce;
        private readonly IInputService _inputService;

        private readonly PlayerConfig _playerConfig;
        private readonly Rigidbody2D _rigidbody;

        private readonly HPPresenter _hpPresenter;
        private readonly DamageableComponent _damageableComponent;
        
        public PlayerPresenter(IUpdaterService updaterService, IInputService inputService, PlayerConfig playerConfig,
            Rigidbody2D rigidbody, HPPresenter hpPresenter, DamageableComponent damageableComponent)
        {
            _updaterService = updaterService;

            _inputService = inputService;
            _playerConfig = playerConfig;
            _rigidbody = rigidbody;

            _directionProviderSerivce = new PlayerDirectionProviderService(_inputService);
            _moverService = new MoverService(_directionProviderSerivce, _playerConfig.LinearSpeed, _rigidbody);

            _hpPresenter = hpPresenter;
            _damageableComponent = damageableComponent;
        }

        public void OnEnable()
        {
            _updaterService.FixedUpdated += FixedUpdate;
            _damageableComponent.Damaged += TakeDamage;
        }

        public void OnDisable()
        {
            _updaterService.FixedUpdated -= FixedUpdate;
            _damageableComponent.Damaged -= TakeDamage;
        }

        public void TakeDamage(float value)
        {
            _hpPresenter.TakeDamage(value);
        }
        
        private void FixedUpdate()
        {
            _moverService.Move(Time.fixedDeltaTime);
        }
    }
}