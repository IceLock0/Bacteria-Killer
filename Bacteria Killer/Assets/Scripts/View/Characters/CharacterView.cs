using Components.Damageable;
using Configs.Entities;
using Presenter.Character;
using Services.Updater;
using UnityEngine;
using View.HP;
using Zenject;

namespace View.Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterView : MonoBehaviour
    {
        protected IUpdaterService UpdaterService;
        
        protected CharacterPresenter CharacterPresenter;

        protected CharacterConfig CharacterConfig;
        
        protected Rigidbody2D Rigidobdy;

        protected HPView HpView;

        protected DamageableComponent DamageableComponent;

        [Inject]
        public void Initialize(IUpdaterService updaterService)
        {
            UpdaterService = updaterService;
            
            Rigidobdy = GetComponent<Rigidbody2D>();
            Rigidobdy.freezeRotation = true;
            Rigidobdy.gravityScale = 0.0f;

            HpView = GetComponentInChildren<HPView>();

            DamageableComponent = GetComponent<DamageableComponent>();
        }

        private void OnEnable()
        {
            CharacterPresenter.OnEnable();
        }

        private void OnDisable()
        {
            CharacterPresenter.OnDisable();
        }
    }
}