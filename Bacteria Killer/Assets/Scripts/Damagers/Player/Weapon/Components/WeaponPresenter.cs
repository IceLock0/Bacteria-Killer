using Configs.Weapon;
using Cysharp.Threading.Tasks;
using Model.Weapon;
using Services.Target;
using Services.Updater;
using View.Weapon;

namespace Damagers.Player.Weapon
{
    public class WeaponPresenter
    {
        private readonly ITargetService _targetService;
        private readonly IUpdaterService _updaterService;

        private readonly WeaponModel _weaponModel;
        private readonly WeaponView _weaponView;

        private readonly WeaponShootSystem _weaponShootSystem;

        public WeaponPresenter(ITargetService targetService , IUpdaterService updaterService, WeaponConfig weaponConfig,
            WeaponView weaponView)
        {
            _targetService = targetService;
            _updaterService = updaterService;

            _weaponModel = new WeaponModel(weaponConfig);
            _weaponView = weaponView;

            _weaponShootSystem = new WeaponShootSystem(this, _targetService);
        }

        public void OnEnable()
        {
            _weaponShootSystem.OnEnable();
            _weaponShootSystem.TargetShooted += _weaponView.ShowShoot;
            _updaterService.Updated += Update;
        }

        public void OnDisable()
        {
            _weaponShootSystem.OnDisable();
            _weaponShootSystem.TargetShooted -= _weaponView.ShowShoot;
            _updaterService.Updated -= Update;
        }

        public bool IsEnoughAmmoToShoot()
            => _weaponModel.IsEnoughAmmoToShoot();

        public void Reload()
            => _weaponModel.Reload();

        public UniTask GetReloadTask() 
            => UniTask.Delay(_weaponModel.ReloadingTimeMs);

        public UniTask GetFireRateTask()
            => UniTask.Delay(_weaponModel.FireRateMs);
        
        public void Shoot()
            => _weaponModel.Shoot();

        public float GetDamage()
            => _weaponModel.Damage;
        
        private void Update()
        {
            _weaponShootSystem.Shoot();
        }
    }
}