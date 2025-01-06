using UnityEngine;

namespace Configs.PillEffects
{
    [CreateAssetMenu(fileName = "IncreaseSpeedEffect Config", menuName = "Configs/PillEffects/IncreaseSpeedEffect", order = 1)]
    public class PillIncreaseSpeedEffectConfig : ScriptableObject
    {
        [SerializeField] private float _value;
        [SerializeField] private float _durationSec;

        public float Value => _value;
        public float DurationSec => _durationSec;
    }
}