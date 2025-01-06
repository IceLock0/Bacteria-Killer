using UnityEngine;

namespace Configs.PillEffects
{
    [CreateAssetMenu(fileName = "PillDecreaseFireRateEffect Config", menuName = "Configs/PillEffects/PillDecreaseFireRateEffect", order = 1)]
    public class PillDecreaseFireRateEffectConfig : ScriptableObject
    {
        [SerializeField] private float _value;
        [SerializeField] private float _durationSec;

        public float Value => _value;
        public float DurationSec => _durationSec;
    }
}