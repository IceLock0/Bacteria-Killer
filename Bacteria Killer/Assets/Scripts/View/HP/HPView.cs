using Configs.Entities;
using Presenter.HP;
using UnityEngine;
using UnityEngine.UI;

namespace View.HP
{
    public class HPView : MonoBehaviour
    {
        private Image _image;
        private float _maxHP;
        
        public void Initialize(CharacterConfig characterConfig)
        {
            _maxHP = characterConfig.MaxHp;
            _image = GetComponentInChildren<Image>();
            Presenter = new HPPresenter(this, _maxHP);
        }

        public HPPresenter Presenter { get; private set; }

        public void UpdateImage(float currentHP)
        {
            _image.fillAmount = currentHP / _maxHP;
        }

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