using UnityEngine;

namespace Configs.Weapon.Shoot
{   
    [CreateAssetMenu(fileName = "Shoot Config", menuName = "Configs/Shoot", order = 0)]
    public class ShootConfig : ScriptableObject
    {
        [SerializeField] private LineRenderer _lineRendererPrefab;
        
        [SerializeField] private float _durationSec;

        public LineRenderer LineRendererPrefab => _lineRendererPrefab;

        public float DurationSec => _durationSec;
    }
}