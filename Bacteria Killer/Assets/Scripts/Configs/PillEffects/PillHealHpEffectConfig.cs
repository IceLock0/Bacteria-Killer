using UnityEngine;

namespace Configs.PillEffects
{
    [CreateAssetMenu(fileName = "PillHealHpEffect Config", menuName = "Configs/PillEffects/PillHealHpEffect", order = 1)]
    public class PillHealHpEffectConfig : ScriptableObject
    {
        [SerializeField] private float _value;

        public float Value => _value;
    }
}