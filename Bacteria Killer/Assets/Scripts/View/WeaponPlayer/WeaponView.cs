using Configs.Weapon;
using Damagers.Player.Weapon;
using Damagers.Player.Weapon.Components.Rotator;
using Services.Detector;
using Services.Movement.PositionProvider;
using Services.Target;
using Services.Updater;
using Services.Upgrade;
using Unity.Mathematics;
using UnityEngine;
using View.Characters.Player;
using Zenject;

namespace View.Weapon
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private bool _isFlipped;

        [SerializeField] private Transform _firePoint;

        private WeaponRotator _weaponRotator;

        private ShootView _shootView;

        [Inject]
        public void Initialize(IClosestObjectFindService closestObjectFindService, IUpdaterService updaterService,
            WeaponConfig weaponConfig, PlayerView playerView,
            IPlayerUpgradeProviderService playerUpgradeProviderService)
        {
            ITargetService playerTargetService =
                new PlayerTargetService(closestObjectFindService, playerView.transform, weaponConfig.Distance);

            Presenter = new WeaponPresenter(playerTargetService, updaterService, weaponConfig, this, playerUpgradeProviderService);

            _weaponRotator = new WeaponRotator(updaterService, playerTargetService, _isFlipped, transform);

            LineRenderer shootPrefab = Instantiate(weaponConfig.ShootConfig.LineRendererPrefab, Vector3.zero,
                quaternion.identity, playerView.transform);

            _shootView = new ShootView(shootPrefab, _firePoint, weaponConfig.ShootConfig.DurationSec);
        }

        public WeaponPresenter Presenter { get; private set; }

        public void ShowShoot(Transform target)
        {
            _shootView.ShowShoot(target);
        }

        private void OnEnable()
        {
            Presenter.OnEnable();
            _weaponRotator.OnEnable();
        }

        private void OnDisable()
        {
            Presenter.OnDisable();
            _weaponRotator.OnEnable();
        }
    }
}