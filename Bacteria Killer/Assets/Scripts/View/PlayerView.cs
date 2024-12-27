using Components.Damageable;
using Configs;
using Presenter.Character.Player;
using Services.Input;
using Services.Updater;
using UnityEngine;
using View.HP;
using Zenject;

namespace View
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour
    {
        private PlayerConfig _playerConfig;

        private Rigidbody2D _rb;
        
        private PlayerPresenter _playerPresenter;

        private HPView _hpView;
        
        [Inject]
        public void Initialize(IUpdaterService updaterService, IInputService inputService, PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;

            _rb = GetComponent<Rigidbody2D>();
            _rb.gravityScale = 0.0f;
            _rb.freezeRotation = true;
            
            _hpView = GetComponentInChildren<HPView>();
            _hpView.Initialize(_playerConfig);

            var damageableComponent = GetComponent<DamageableComponent>();
            
            _playerPresenter = new PlayerPresenter(updaterService, inputService, _playerConfig, _rb, _hpView.Presenter, damageableComponent);
        }
        

        private void OnEnable()
        {
            _playerPresenter.OnEnable();
        }

        private void OnDisable()
        {
            _playerPresenter.OnDisable();
        }
    }
}