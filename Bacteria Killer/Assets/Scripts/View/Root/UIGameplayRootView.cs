using System;
using Data;
using Services.Destroyer;
using Services.SaveLoad;
using UnityEngine;
using View.Buttons.Gameplay;
using View.Characters.Player;
using Zenject;

namespace View.Root
{
    public class UIGameplayRootView : MonoBehaviour
    {
        [SerializeField] private HomeButtonView _homeButtonView;

        [SerializeField] private GameObject _defeatScreen;

        private IGameObjectDestroyerService _gameObjectDestroyerService;
        private ISaveLoadService _saveLoadService;
        
        public event Action HomeButtonPressed;

        [Inject]
        public void Initialize(IGameObjectDestroyerService gameObjectDestroyerService, ISaveLoadService saveLoadService)
        {
            _gameObjectDestroyerService = gameObjectDestroyerService;
            _saveLoadService = saveLoadService;
        }
        
        private void Awake()
        {
            _defeatScreen.SetActive(false);
        }

        private void OnPlayerDestroy(GameObject go)
        {
            if (!go.TryGetComponent<PlayerView>(out var _))
                return;
            
            _defeatScreen.SetActive(true);
        }
        
        private void OnEnable()
        {
            _homeButtonView.Clicked += (() => HomeButtonPressed?.Invoke());
            _gameObjectDestroyerService.Destroyed += OnPlayerDestroy;
        }

        private void OnDisable()
        {
            _homeButtonView.Clicked -= (() => HomeButtonPressed?.Invoke());
            _gameObjectDestroyerService.Destroyed -= OnPlayerDestroy;

        }
    }
}