using Configs.Weapon;
using Cysharp.Threading.Tasks;
using Model.Weapon;
using Services.Audio;
using Services.Target;
using Services.Updater;
using Services.Upgrade;
using UnityEngine;
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

        private readonly IPlayerUpgradeProviderService _playerUpgradeProviderService;
        
        public WeaponPresenter(ITargetService targetService , IUpdaterService updaterService, WeaponConfig weaponConfig,
            WeaponView weaponView, IPlayerUpgradeProviderService playerUpgradeProviderService)
        {
            _targetService = targetService;
            _updaterService = updaterService;

            _weaponModel = new WeaponModel(weaponConfig);
            _weaponView = weaponView;

            _weaponShootSystem = new WeaponShootSystem(this, _targetService);
            
            _playerUpgradeProviderService = playerUpgradeProviderService;
        }

        public void OnEnable()
        {
            _weaponShootSystem.OnEnable();
            _weaponShootSystem.TargetShooted += _weaponView.ShowShoot;
            _updaterService.Updated += Update;

            _playerUpgradeProviderService.DamageUpgraded += IncreaseDamage;
        }

        public void OnDisable()
        {
            _weaponShootSystem.OnDisable();
            _weaponShootSystem.TargetShooted -= _weaponView.ShowShoot;
            _updaterService.Updated -= Update;
            
            _playerUpgradeProviderService.DamageUpgraded -= IncreaseDamage;
        }

        public void IncreaseFireRate(float value)
        {
            _weaponModel.IncreaseFireRate(value);
        }
        
        public void DecreaseFireRate(float value)
        {
            _weaponModel.DecreaseFireRate(value);
        }
        
        public bool IsEnoughAmmoToShoot()
            => _weaponModel.IsEnoughAmmoToShoot();

        public void Reload()
            => _weaponModel.Reload();

        public UniTask GetReloadTask()
        {
            _weaponView.ShowReload((float)_weaponModel.ReloadingTimeMs / 1000);
            
            return UniTask.Delay(_weaponModel.ReloadingTimeMs);
        }

        public UniTask GetFireRateTask()
             => UniTask.Delay(_weaponModel.FireRateMs);


        public void Shoot()
        {
            _weaponModel.Shoot();
            _weaponView.ChangeAmmo(_weaponModel.CurrentAmmo);
        }

        public float GetDamage()
            => _weaponModel.Damage;

        private void IncreaseDamage(float value)
        {
            Debug.Log("Increase Damage");
            _weaponModel.IncreaseDamage(value);
        }
        
        private void Update()
        {
            _weaponShootSystem.Shoot();
        }
    }
}