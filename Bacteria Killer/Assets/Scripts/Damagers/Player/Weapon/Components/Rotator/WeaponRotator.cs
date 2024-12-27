using Services.Target;
using Services.Updater;
using UnityEngine;

namespace Damagers.Player.Weapon.Components.Rotator
{
    public class WeaponRotator
    {
        private readonly IUpdaterService _updaterService;
        private readonly ITargetService _targetService;
    
        private readonly bool _isFlipped;
        private readonly Transform _weaponTransform;
    
        private Vector2 _target;
    
        public WeaponRotator(IUpdaterService updaterService, ITargetService targetService, bool isFlipped, Transform weaponTransform)
        {
            _updaterService = updaterService;
            _targetService = targetService;
        
            _isFlipped = isFlipped;
            _weaponTransform = weaponTransform;
        }

        public void OnEnable()
        {
            _updaterService.Updated += Update;
        }

        public void OnDisable()
        {
            _updaterService.Updated -= Update;
        }

        private void Update()
        {
            Rotate();
        }
        
        private void Rotate()
        {
            var closestEnemy = _targetService.GetTarget();
            
            if (closestEnemy == null)
                return;

            _target = closestEnemy.transform.position;

            var direction = _target - (Vector2)_weaponTransform.position;

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (_isFlipped)
                angle -= 180;
        
            _weaponTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
