using System;
using Components.Damageable;
using Configs.Entities;
using Cysharp.Threading.Tasks;
using Presenter.Character;
using Services.Audio;
using Services.Destroyer;
using Services.Updater;
using UnityEngine;
using View.HP;
using Zenject;

namespace View.Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class CharacterView : MonoBehaviour
    {
        protected IUpdaterService UpdaterService;
        protected IGameObjectDestroyerService GameObjectDestroyerService;
        
        protected CharacterConfig CharacterConfig;
        
        protected Rigidbody2D Rigidobdy;

        protected HPView HpView;

        protected DamageableComponent DamageableComponent;

        protected IAudioService AudioService;
        
        [Inject]
        public void Initialize(IUpdaterService updaterService, IGameObjectDestroyerService gameObjectDestroyerService, IAudioService audioService)
        {
            UpdaterService = updaterService;
            GameObjectDestroyerService = gameObjectDestroyerService;
            
            Rigidobdy = GetComponent<Rigidbody2D>();
            Rigidobdy.freezeRotation = true;
            Rigidobdy.gravityScale = 0.0f;

            HpView = GetComponentInChildren<HPView>();

            DamageableComponent = GetComponent<DamageableComponent>();
            
            AudioService = audioService;
        }

        public CharacterPresenter Presenter { get; protected set; }

        public abstract void ShowDeath();
        
        private void OnEnable()
        {
            Presenter.OnEnable();
        }

        private void OnDisable()
        {
            Presenter.OnDisable();
        }
    }
}