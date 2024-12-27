using Configs.Weapon;
using Damagers.Player.Weapon;
using Damagers.Player.Weapon.Components.Rotator;
using Services.Detector;
using Services.Movement.PositionProvider;
using Services.Target;
using Services.Updater;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace View.Weapon
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private bool _isFlipped;
        
        [SerializeField] private Transform _firePoint;
        
        private WeaponPresenter _weaponPresenter;
        
        private WeaponRotator _weaponRotator;

        private ShootView _shootView;
        
        [Inject]
        public void Initialize(IClosestObjectFindService closestObjectFindService, IUpdaterService updaterService, WeaponConfig weaponConfig, PlayerView playerView)
        {
            ITargetService playerTargetService = new PlayerTargetService(closestObjectFindService, playerView.transform);

            _weaponPresenter = new WeaponPresenter(playerTargetService, updaterService, weaponConfig, this);
            
            _weaponRotator = new WeaponRotator(updaterService, playerTargetService, _isFlipped, transform);

            LineRenderer shootPrefab = Instantiate(weaponConfig.ShootConfig.LineRendererPrefab, Vector3.zero, quaternion.identity, playerView.transform);

            _shootView = new ShootView(shootPrefab, _firePoint, weaponConfig.ShootConfig.DurationSec);
        }

        public void ShowShoot(Transform target)
        {
            _shootView.ShowShoot(target);
        }
        
        private void OnEnable()
        {
            _weaponPresenter.OnEnable();
            _weaponRotator.OnEnable();
        }

        private void OnDisable()
        {
            _weaponPresenter.OnDisable();
            _weaponRotator.OnEnable();
        }
    }
}