using Components.Damageable;
using Services.Target;
using UnityEngine;

namespace Damagers.Player.Weapon
{
    public class TargetChecker: WeaponChecker
    {
        private readonly ITargetService _targetService;
        
        public TargetChecker(ITargetService targetService)
        {
            _targetService = targetService;
        }
        
        public override void Check()
        {
            var target = _targetService.GetTarget();

            if (target != null && target.TryGetComponent<IDamageable>(out _))
                base.Check();
        }
    }
}