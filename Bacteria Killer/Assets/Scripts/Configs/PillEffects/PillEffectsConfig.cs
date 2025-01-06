using UnityEngine;

namespace Configs.PillEffects
{
    [CreateAssetMenu(fileName = "PillEffect Config", menuName = "Configs/PillEffects/Main", order = 0)]
    public class PillEffectsConfig : ScriptableObject
    {
        [SerializeField] PillDecreaseFireRateEffectConfig _pillDecreaseFireRateEffectConfig;
        [SerializeField] PillIncreaseSpeedEffectConfig _pillIncreaseSpeedEffectConfig;
        [SerializeField] PillHealHpEffectConfig _pillHealHpEffectConfig;
        [SerializeField] PillShockWaveEffectConfig _pillShockWaveEffectConfig;

        public PillDecreaseFireRateEffectConfig PillDecreaseFireRateEffectConfig => _pillDecreaseFireRateEffectConfig;
        public PillIncreaseSpeedEffectConfig PillIncreaseSpeedEffectConfig => _pillIncreaseSpeedEffectConfig;
        public PillHealHpEffectConfig PillHealHpEffectConfig => _pillHealHpEffectConfig;
        public PillShockWaveEffectConfig PillShockWaveEffectConfig => _pillShockWaveEffectConfig;
    }
}