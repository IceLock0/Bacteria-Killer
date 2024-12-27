using System;
using Model.HP;
using View.HP;

namespace Presenter.HP
{
    public class HPPresenter
    {
        private readonly HPModel _model;
        private readonly HPView _view;

        public HPPresenter(HPView view, float maxHP)
        {
            _view = view;
            _model = new HPModel(maxHP);
        }

        public event Action Destroyed;
        
        public void OnEnable()
        {
            _model.Changed += _view.UpdateImage;
            _model.Died += DestroyEntity;
        }
        
        public void OnDisable()
        {
            _model.Changed -= _view.UpdateImage;
        }

        public void TakeDamage(float value)
        {
            _model.TakeDamage(value);
        }

        private void DestroyEntity()
        {
            _view.Destroy();
            Destroyed?.Invoke();
        }
    }
}