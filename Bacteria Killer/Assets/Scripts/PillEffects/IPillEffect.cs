using UnityEngine;

namespace PillEffects
{
    public interface IPillEffect
    {
        public void Apply(Collider2D collider);
    }
}