using System;
using UnityEngine;
using UnityEngine.UI;
using Utils.Extensions;

namespace View.GameplayButtons
{
    public class HomeButtonView : MonoBehaviour
    {
        [SerializeField] private Button _homeButton;

        public event Action Clicked; 
        
        private void OnEnable()
        {
            _homeButton.AddListener(() => Clicked?.Invoke());
        }

        private void OnDisable()
        {
            _homeButton.RemoveListener(() => Clicked?.Invoke());
        }
    }
}