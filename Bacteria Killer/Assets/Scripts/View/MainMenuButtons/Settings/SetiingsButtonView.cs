using System;
using UnityEngine;
using UnityEngine.UI;
using Utils.Extensions;

namespace View.MainMenuButtons
{
    public class SetiingsButtonView : MonoBehaviour
    {
        [SerializeField] private Button _settingsButton;

        [SerializeField] private GameObject _settings;

        private void Awake()
        {
            gameObject.SetActive(true);
        }

        private void OpenSettings()
        {
            _settings.SetActive(true);
            gameObject.SetActive(false);
        } 
        
        private void OnEnable()
        {
            _settingsButton.AddListener(OpenSettings);
        }
        
        private void OnDisable()
        {
            _settingsButton.RemoveListener(OpenSettings);
        }
    }
}