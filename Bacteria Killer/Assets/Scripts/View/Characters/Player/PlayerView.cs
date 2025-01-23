using Configs;
using Presenter.Character.Player;
using Services.Input;
using Services.Upgrade;
using UnityEngine;
using Zenject;

namespace View.Characters.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : CharacterView
    {
        [SerializeField] private ParticleSystem _particleSystem;    
        
        private PlayerConfig _playerConfig;

        private PlayerPresenter _playerPresenter;

        [Inject]
        public void Initialize(IInputService inputService, PlayerConfig playerConfig, IPlayerUpgradeProviderService playerUpgradeProviderService)
        {
            _playerConfig = playerConfig;
            CharacterConfig = _playerConfig;

            HpView.Initialize(CharacterConfig,playerUpgradeProviderService, true);

            _playerPresenter = new PlayerPresenter(this, UpdaterService, inputService, _playerConfig, Rigidobdy,
                HpView.Presenter, DamageableComponent, GameObjectDestroyerService, playerUpgradeProviderService);
            Presenter = _playerPresenter;
        }
        
        public override void ShowDeath()
        {
            ParticleSystem createdParticles = Instantiate(_particleSystem, transform.position, Quaternion.identity);
            
            createdParticles.Play();
        }
    }
}