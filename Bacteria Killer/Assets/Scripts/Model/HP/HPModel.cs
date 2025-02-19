﻿using System;
using UnityEngine;

namespace Model.HP
{
    public class HPModel
    {
        private float _maxHP;
        private float _currentHP;

        public HPModel(float maxHP)
        {
            _maxHP = maxHP;
            _currentHP = _maxHP;
        }
        
        public event Action<float, float> Changed;
        public event Action Died;

        public void TakeDamage(float value)
        {
            _currentHP -= value;
            
            if (_currentHP <= 0)
                Died?.Invoke();
            
            else Changed?.Invoke(_currentHP, _maxHP);
        }

        public void Heal(float value)
        {
            _currentHP = Mathf.Clamp(_currentHP, _currentHP + value, _maxHP);
            Changed?.Invoke(_currentHP, _maxHP);
        }

        public void IncreaseMaxHp(float value)
        {
            _maxHP += value;
            Heal(_maxHP);
        }
    }
}