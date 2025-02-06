using Configs.Entities;

namespace Model.Characters
{
    public abstract class CharacterModel
    {
        public CharacterModel(CharacterConfig characterConfig)
        {
            LinearSpeed = characterConfig.LinearSpeed;
            MaxHp = characterConfig.MaxHp;
        }

        public float LinearSpeed { get; protected set; }
        public float MaxHp { get; protected set; }

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