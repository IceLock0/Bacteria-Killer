using System;
using Entities.Components;
using UnityEngine;
using UnityEngine.UI;

namespace View.HP
{
    public class HPView : IDisposable
    {
        private readonly HPComponent _hp;
        
        private readonly Image _image;
        private readonly float _maxHP;
        
        public HPView(HPComponent hp, Image image, float maxHP)
        {
            _hp = hp;
            _hp.Changed += UpdateImage;
            
            _image = image;

            _maxHP = maxHP;
        }

        private void UpdateImage(float currentHP)
        {
            _image.fillAmount = currentHP / _maxHP;
        }

        public void Dispose()
        {
            _hp.Changed -= UpdateImage;
        }
    }
}