using Components.Damageable;
using Configs;
using Model.Characters.Player;
using Presenter.HP;
using Services.Destroyer;
using Services.Input;
using Services.Movement.DirectionProvider.Player;
using Services.Movement.Mover;
using Services.Updater;
using Services.Upgrade;
using UnityEngine;
using View.Characters.Player;

namespace Presenter.Character.Player
{
    public class PlayerPresenter : CharacterPresenter
    {
        private readonly PlayerModel _playerModel;

        private readonly IPlayerUpgradeProviderService _playerUpgradeProviderService;

        public PlayerPresenter(PlayerView playerView, IUpdaterService updaterService, IInputService inputService,
            PlayerConfig playerConfig,
            Rigidbody2D rigidbody, HPPresenter hpPresenter, DamageableComponent damageableComponent,
            IGameObjectDestroyerService gameObjectDestroyerService,
            IPlayerUpgradeProviderService playerUpgradeProviderService)
            : base(playerView, updaterService, hpPresenter, damageableComponent, gameObjectDestroyerService)
        {
            _playerModel = new PlayerModel(playerConfig);
            CharacterModel = _playerModel;

            var playerDirectionProviderService = new PlayerDirectionProviderService(inputService);
            MoverService = new MoverService(playerDirectionProviderService, rigidbody);

            _playerUpgradeProviderService = playerUpgradeProviderService;
        }

        public override void OnEnable()
        {
            base.OnEnable();
            
            _playerUpgradeProviderService.SpeedUpgraded += IncreaseSpeed;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            
            _playerUpgradeProviderService.SpeedUpgraded -= IncreaseSpeed;
        }
    }
}