using Configs;
using UnityEngine;

namespace Editor
{
    public class PlayerDebug : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;

        private void OnDrawGizmos()
        {
            //detector
            Gizmos.DrawWireCube(transform.position, _playerConfig.DetectorSize);
        }
    }
}