using Configs.Entities;
using Presenter.HP;
using Services.Upgrade;
using UnityEngine;
using UnityEngine.UI;

namespace View.HP
{
    public class HPView : MonoBehaviour
    {
        private Image _image;

        public void Initialize(CharacterConfig characterConfig, IPlayerUpgradeProviderService playerUpgradeProviderService, bool isPlayer = false)
        {
            _image = GetComponentInChildren<Image>();
            Presenter = new HPPresenter(this, characterConfig, playerUpgradeProviderService, isPlayer);
        }

        public HPPresenter Presenter { get; private set; }

        public void UpdateImage(float currentHP, float maxHp)
        {
            _image.fillAmount = currentHP / maxHp;
        }

        public void Destroy()
        {
            //show death
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