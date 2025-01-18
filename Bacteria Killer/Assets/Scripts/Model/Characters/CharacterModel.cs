using Configs.Entities;
using UnityEngine;

namespace Model.Characters
{
    public abstract class CharacterModel
    {
        public CharacterModel(CharacterConfig characterConfig)
        {
            LinearSpeed = characterConfig.LinearSpeed;
            MaxHp = characterConfig.MaxHp;
        }

        public float LinearSpeed { get; private set; }
        public float MaxHp { get; private set; }

        public void IncreaseSpeed(float value)
        {
            LinearSpeed += value;
        }
        
        public void DecreaseSpeed(float value)
        {
            LinearSpeed -= value;
        }
    }
}