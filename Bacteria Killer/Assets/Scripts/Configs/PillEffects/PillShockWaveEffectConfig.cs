using UnityEngine;

namespace Configs.PillEffects
{
    [CreateAssetMenu(fileName = "PillShockWaveEffectConfig Config", menuName = "Configs/PillEffects/PillShockWaveEffect", order = 1)]
    public class PillShockWaveEffectConfig : ScriptableObject
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _damageValue;

        public float Radius => _radius;
        public float DamageValue => _damageValue;
    }
}