using UnityEngine;


namespace Configs.Entities
{
    public class CharacterConfig : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private float _linearSpeed;

        [Header("Stats")] 
        [SerializeField] private float _maxHp;
        
        public float LinearSpeed => _linearSpeed;

        public float MaxHp => _maxHp;
    }
}