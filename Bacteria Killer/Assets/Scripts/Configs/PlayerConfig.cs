using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Configs", menuName = "Configs/Player", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float _linearSpeed;

        [SerializeField] private Vector2 _detectorSize;
        
        public float LinearSpeed => _linearSpeed;

        public Vector2 DetectorSize => _detectorSize;
    }
}