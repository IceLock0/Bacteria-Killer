using UnityEngine;

namespace Editor
{
    public class PlayerDebug : MonoBehaviour
    {
        [SerializeField] private Vector2 _detectorSize;

        private void OnDrawGizmos()
        {
            //detector
            Gizmos.DrawWireCube(transform.position, _detectorSize);
        }
    }
}