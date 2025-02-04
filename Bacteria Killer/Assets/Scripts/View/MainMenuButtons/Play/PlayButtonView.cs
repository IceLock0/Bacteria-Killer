using System;
using UnityEngine;
using UnityEngine.UI;
using Utils.Extensions;

namespace View.MainMenuButtons
{
    public class PlayButtonView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        public event Action Clicked; 
        
        private void OnEnable()
        {
            _playButton.AddListener(() => Clicked?.Invoke());
        }

        private void OnDisable()
        {
            _playButton.RemoveListener(() => Clicked?.Invoke());
        }
    }
}