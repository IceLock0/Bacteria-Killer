using System;
using Configs.Entities;
using Enemy.Components;
using Entities.Components;
using Movement;
using UnityEngine;
using UnityEngine.UI;
using View.HP;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Entity : MonoBehaviour, IDamageable
    {
        [SerializeField] private Image _hpImage;

        protected EntityConfig Config;
        protected IDirectionProvider DirectionProvider;

        private Mover _mover;
        
        private HPComponent _hp;
        private HPView _hpView;
        
        public void TakeDamage(float value)
        {
            _hp.TakeDamage(value);
        }

        private void Awake()
        {
            _hp = new HPComponent(Config.HP);
            _hpView = new HPView(_hp, _hpImage, Config.HP);

            _mover = new Mover(DirectionProvider, Config, GetComponent<Rigidbody2D>());
        }

        private void FixedUpdate()
        {
            _mover.Move(Time.fixedDeltaTime);
        }
    }
}