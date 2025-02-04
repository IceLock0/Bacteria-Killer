using System;
using UnityEngine;
using View.MainMenuButtons;

namespace View.Root
{
    public class UIMainMenuRootView : MonoBehaviour
    {
        [SerializeField] private PlayButtonView _playButtonView;

        public event Action PlayButtonPressed;
        
        private void OnEnable()
        {
            _playButtonView.Clicked += (() => PlayButtonPressed?.Invoke());
        }

        private void OnDisable()
        {
            _playButtonView.Clicked -= (() => PlayButtonPressed?.Invoke());
        }
    }
}