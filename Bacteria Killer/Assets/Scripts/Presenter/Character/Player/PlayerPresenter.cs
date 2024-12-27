using Components.Damageable;
using Configs;
using Model.Characters.Player;
using Presenter.HP;
using Services.Input;
using Services.Movement.DirectionProvider.Player;
using Services.Movement.Mover;
using Services.Updater;
using UnityEngine;

namespace Presenter.Character.Player
{
    public class PlayerPresenter : CharacterPresenter
    {
        private readonly PlayerModel _playerModel;

        public PlayerPresenter(IUpdaterService updaterService, IInputService inputService, PlayerConfig playerConfig,
            Rigidbody2D rigidbody, HPPresenter hpPresenter, DamageableComponent damageableComponent) 
            : base(updaterService, hpPresenter, damageableComponent)
        {
            _playerModel = new PlayerModel(playerConfig);
            
            var playerDirectionProviderService = new PlayerDirectionProviderService(inputService);
            MoverService = new MoverService(playerDirectionProviderService, _playerModel.LinearSpeed, rigidbody);
        }
    }
}