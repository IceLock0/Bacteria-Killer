using Configs.Entities;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Configs/Entities/Player", order = 0)]
    public class PlayerConfig : EntityConfig
    {
        [SerializeField] private Vector2 _detectorSize;

        public Vector2 DetectorSize => _detectorSize;
    }
}