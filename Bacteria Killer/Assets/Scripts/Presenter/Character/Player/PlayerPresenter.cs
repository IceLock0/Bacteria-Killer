using Components.Damageable;
using Configs;
using Model.Characters.Player;
using Presenter.HP;
using Services.Destroyer;
using Services.Input;
using Services.Movement.DirectionProvider.Player;
using Services.Movement.Mover;
using Services.Updater;
using UnityEngine;
using View.Characters.Player;

namespace Presenter.Character.Player
{
    public class PlayerPresenter : CharacterPresenter
    {
        private readonly PlayerModel _playerModel;

        public PlayerPresenter(PlayerView playerView, IUpdaterService updaterService, IInputService inputService, PlayerConfig playerConfig,
            Rigidbody2D rigidbody, HPPresenter hpPresenter, DamageableComponent damageableComponent, IGameObjectDestroyerService gameObjectDestroyerService) 
            : base(playerView, updaterService, hpPresenter, damageableComponent, gameObjectDestroyerService)
        {
            _playerModel = new PlayerModel(playerConfig);
            CharacterModel = _playerModel;
            
            var playerDirectionProviderService = new PlayerDirectionProviderService(inputService);
            MoverService = new MoverService(playerDirectionProviderService, rigidbody);
        }
    }
}