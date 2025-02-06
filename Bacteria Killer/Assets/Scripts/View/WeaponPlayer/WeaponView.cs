using System.Threading;
using Configs.Weapon;
using Damagers.Player.Weapon;
using Damagers.Player.Weapon.Components.Rotator;
using DG.Tweening;
using Services.Audio;
using Services.Detector;
using Services.Target;
using Services.Updater;
using Services.Upgrade;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using View.Characters.Player;
using Zenject;

namespace View.Weapon
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private bool _isFlipped;

        [SerializeField] private Transform _firePoint;

        [SerializeField] private TextMeshProUGUI _ammoText;
        [SerializeField] private Image _reloadImage;

        private WeaponRotator _weaponRotator;

        private ShootView _shootView;

        private readonly CancellationTokenSource _cts = new();
        
        private IAudioService _audioService;
        private const string AUDIO_SHOOT = "Shoot";
        
        [Inject]
        public void Initialize(IClosestObjectFindService closestObjectFindService, IUpdaterService updaterService,
            WeaponConfig weaponConfig, PlayerView playerView,
            IPlayerUpgradeProviderService playerUpgradeProviderService, IAudioService audioService)
        {
            ITargetService playerTargetService =
                new PlayerTargetService(closestObjectFindService, playerView.transform, weaponConfig.Distance);

            Presenter = new WeaponPresenter(playerTargetService, updaterService, weaponConfig, this,
                playerUpgradeProviderService);

            _weaponRotator = new WeaponRotator(updaterService, playerTargetService, _isFlipped, transform);

            LineRenderer shootPrefab = Instantiate(weaponConfig.ShootConfig.LineRendererPrefab, Vector3.zero,
                quaternion.identity, playerView.transform);


            _shootView = new ShootView(shootPrefab, _firePoint, weaponConfig.ShootConfig.DurationSec, _cts.Token);

            _audioService = audioService;
            
            _reloadImage.fillAmount = 0f;
            _ammoText.text = weaponConfig.AmmoCapacity.ToString();
        }

        public WeaponPresenter Presenter { get; private set; }

        public void ShowShoot(Transform target)
        {
            _audioService.Play(AUDIO_SHOOT);
            _shootView.ShowShoot(target);
        }

        public void ChangeAmmo(int ammo)
        {
            _ammoText.text = ammo <= 0 ? 0.ToString() : ammo.ToString();
        }

        public void ShowReload(float durationSec)
        {
            _reloadImage.fillAmount = 1;
            _reloadImage.DOFillAmount(0, durationSec);
        }

        private void OnEnable()
        {
            Presenter.OnEnable();
            _weaponRotator.OnEnable();
        }

        private void OnDisable()
        {
            Presenter.OnDisable();
            _weaponRotator.OnDisable();

            _cts.Cancel();
        }
    }
}