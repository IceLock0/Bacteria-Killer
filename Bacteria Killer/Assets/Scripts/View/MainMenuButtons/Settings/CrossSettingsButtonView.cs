using System;
using UnityEngine;
using UnityEngine.UI;
using Utils.Extensions;

namespace View.MainMenuButtons
{
    public class CrossSettingsButtonView : MonoBehaviour
    {
        [SerializeField] private Button _crossButon;
        
        [SerializeField] private GameObject _button;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void CloseSettings()
        { 
            _button.SetActive(true);
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _crossButon.AddListener(CloseSettings);
        }

        private void OnDisable()
        {
            _crossButon.RemoveListener(CloseSettings);
        }
    }
}