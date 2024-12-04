using Configs.Entities;
using Enemy.Components;
using Entities.Components;
using UnityEngine;
using UnityEngine.UI;
using View.HP;
using Zenject;

namespace Enemy
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private Image _hpImage;  
        
        private HPComponent _hp;
        private HPView _hpView;
        
        [Inject]
        public void Initialize(EnemyConfig enemyConfig)
        {
            _hp = new HPComponent(enemyConfig.HP);
            _hpView = new HPView(_hp, _hpImage, enemyConfig.HP);
        }

        public void TakeDamage(float value)
        {
            _hp.TakeDamage(value);
        }
    }
}