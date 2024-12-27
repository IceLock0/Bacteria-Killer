using Configs.Entities;

namespace Model.Characters.Enemy
{
    public class EnemyModel : CharacterModel
    {
        public EnemyModel(EnemyConfig enemyConfig) : base(enemyConfig)
        {
                SetEnemyConfigData(enemyConfig);
        }

        private void SetEnemyConfigData(EnemyConfig enemyConfig)
        {
            AttackDistance = enemyConfig.AttackDistance;
            AttackDamage = enemyConfig.AttackDamage;
            Difficult = enemyConfig.Difficult;
        }
        
        public float AttackDistance { get; private set; }
        public float AttackDamage { get; private set; }
        public float Difficult { get; private set; }
    }
}