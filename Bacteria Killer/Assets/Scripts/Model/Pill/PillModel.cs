using System.Collections.Generic;
using PillEffects;

namespace Model.Pill
{
    public class PillModel
    {
        public PillModel(List<IPillEffect> effects)
        {
            Effects = effects;
        }

        public List<IPillEffect> Effects { get; } = new();
    }
}