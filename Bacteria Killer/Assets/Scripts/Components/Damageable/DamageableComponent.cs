using System;
using UnityEngine;

namespace Components.Damageable
{
    public class DamageableComponent : MonoBehaviour, IDamageable
    {
        public event Action<float> Damaged;
        
        public void TakeDamage(float value)
        {
            Damaged?.Invoke(value);
        }
    }
}