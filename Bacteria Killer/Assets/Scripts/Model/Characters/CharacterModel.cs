using Configs.Entities;

namespace Model.Characters
{
    public abstract class CharacterModel
    {
        private float _linearSpeed;
        private float _maxHp;
        
        public CharacterModel(CharacterConfig characterConfig)
        {
            LinearSpeed = characterConfig.LinearSpeed;
            MaxHp = characterConfig.MaxHp;
        }

        public float LinearSpeed { get; private set; }
        public float MaxHp { get; private set; }
    }
}