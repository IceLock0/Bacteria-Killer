using UnityEngine;

namespace Animations.Transform
{
    public class CircleMover : MonoBehaviour
    {
        [SerializeField] private Vector2 _radius;
        [SerializeField] private float _speed ;
        
        private float _time = 0;

        private void Update()
        {
            _time += Time.deltaTime * _speed;
            
            transform.position =  new Vector3(Mathf.Sin(_time) * _radius.x, Mathf.Cos(_time) * _radius.y, 0);
        }
    }
}