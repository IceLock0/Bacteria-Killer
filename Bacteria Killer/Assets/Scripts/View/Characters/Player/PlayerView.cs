﻿using System;
using Configs;
using Presenter.Character.Player;
using Services.Input;
using Services.Upgrade;
using UnityEngine;
using Zenject;

namespace View.Characters.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : CharacterView
    {
        [SerializeField] private ParticleSystem _particleSystem;    
        
        private PlayerConfig _playerConfig;

        private PlayerPresenter _playerPresenter;
        
        private const string AUDIO_GAME_OVER = "GameOver";
        
        [Inject]
        public void Initialize(IInputService inputService, PlayerConfig playerConfig, IPlayerUpgradeProviderService playerUpgradeProviderService)
        {
            _playerConfig = playerConfig;
            CharacterConfig = _playerConfig;

            HpView.Initialize(CharacterConfig,playerUpgradeProviderService, true);

            _playerPresenter = new PlayerPresenter(this, UpdaterService, inputService, _playerConfig, Rigidobdy,
                HpView.Presenter, DamageableComponent, GameObjectDestroyerService, playerUpgradeProviderService);
            Presenter = _playerPresenter;
        }
        
        public override void ShowDeath()
        {
            AudioService.Play(AUDIO_GAME_OVER);
            
            Camera.main?.gameObject.transform.SetParent(null);
            
            ParticleSystem createdParticles = Instantiate(_particleSystem, transform.position, Quaternion.identity);
            
            createdParticles.Play();
        }
    }
}