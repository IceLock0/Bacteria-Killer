using UnityEngine;


namespace Configs.Entities
{
    public class EntityConfig : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private float _linearSpeed;

        [Header("Stats")] 
        [SerializeField] private float _hp;
        
        public float LinearSpeed => _linearSpeed;

        public float HP => _hp;
    }
}