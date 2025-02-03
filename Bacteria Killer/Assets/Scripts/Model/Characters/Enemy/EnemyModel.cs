using Configs.Entities;

namespace Model.Characters.Enemy
{
    public class EnemyModel : CharacterModel
    {
        public EnemyModel(EnemyConfig enemyConfig, float bossScaler) : base(enemyConfig)
        {
            AttackDistance = enemyConfig.AttackDistance;
            AttackDamage = enemyConfig.AttackDamage * bossScaler;
            Difficult = enemyConfig.Difficult * bossScaler;
            LinearSpeed *= bossScaler;
            MaxHp *= bossScaler;
        }
        
        public float AttackDistance { get; private set; }
        public float AttackDamage { get; private set; }
        public float Difficult { get; private set; }
    }
}