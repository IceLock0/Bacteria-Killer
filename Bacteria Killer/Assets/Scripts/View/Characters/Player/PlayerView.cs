using Configs;
using Presenter.Character.Player;
using Services.Input;
using UnityEngine;
using Zenject;

namespace View.Characters.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : CharacterView
    {
        private PlayerConfig _playerConfig;

        private PlayerPresenter _playerPresenter;

        [Inject]
        public void Initialize(IInputService inputService, PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
            CharacterConfig = _playerConfig;

            HpView.Initialize(CharacterConfig);

            _playerPresenter = new PlayerPresenter(this, UpdaterService, inputService, _playerConfig, Rigidobdy,
                HpView.Presenter, DamageableComponent, GameObjectDestroyerService);
            Presenter = _playerPresenter;
        }
    }
}