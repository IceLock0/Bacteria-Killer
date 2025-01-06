using System.Collections.Generic;
using Enums.Pill;
using PillEffects;
using UnityEngine;
using View.Pill;

namespace Utils.Factory.PillFactory
{
    public interface IPillFactory
    {
        public PillView Create(List<IPillEffect> pillEffects, PillColorType colorType, Vector2 at, Quaternion rotation, Transform parent = null );
    }
}