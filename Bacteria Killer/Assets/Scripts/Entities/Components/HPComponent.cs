using System;

namespace Entities.Components
{
    public class HPComponent
    {
        private readonly float _maxHP;
        private float _currentHP;
    
        public HPComponent(float maxHP)
        {
            _maxHP = maxHP;
            _currentHP = _maxHP;
        }

        public event Action<float> Changed;
        public event Action Died;
        
        public void TakeDamage(float value)
        {
            _currentHP -= value;
            
            Changed?.Invoke(_currentHP);
            
            if(_currentHP <= 0)
                Died?.Invoke();
        }
    }
}