using Components.Damageable;
using UnityEngine;

namespace PillEffects
{
    public class PillShockWaveEffect : IPillEffect
    {
        private readonly float _radius;
        private readonly float _shockDamageValue;
        
        public PillShockWaveEffect(float radius, float shockDamageValue)
        {
            _radius = radius;
            _shockDamageValue = shockDamageValue;
        }
        
        public void Apply(Collider2D collider)
        {
            var colliders = Physics2D.OverlapCircleAll(collider.gameObject.transform.position, _radius);
            
            MakeExplosion(colliders, collider);
        }

        private void MakeExplosion(Collider2D[] colliders, Collider2D sourceCollider)
        {
            foreach (Collider2D collider in colliders)
            {
                if(collider == sourceCollider)
                    continue;

                if(collider.TryGetComponent<IDamageable>(out var damageable))
                    damageable.TakeDamage(_shockDamageValue);   
            }
        }
    }
}