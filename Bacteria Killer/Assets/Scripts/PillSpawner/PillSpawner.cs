using System;
using System.Collections.Generic;
using Configs.PillEffects;
using Configs.PillSpawn;
using Enums.Pill;
using PillEffects;
using Services.Destroyer;
using UnityEngine;
using Utils.Factory.PillFactory;
using Utils.RandomPosition;
using View.Pill;
using Random = UnityEngine.Random;

namespace PillSpawner
{
    public class PillSpawner
    {
        private readonly IPillFactory _pillFactory;

        private readonly Dictionary<PillColorType, List<IPillEffect>> _pillEffects = new();

        private readonly IGameObjectDestroyerService _gameObjectDestroyerService;

        private readonly Transform _playerTransform;

        private readonly float _minDistance;
        private readonly float _maxDistance;

        private readonly float _maxPills;
        private int _currentPills = 0;

        private readonly PillEffectsConfig _pillEffectsConfig;

        public PillSpawner(IPillFactory pillFactory, PillSpawnerConfig pillSpawnerConfig,
            IGameObjectDestroyerService gameObjectDestroyerService, Transform playerTransform,
            PillEffectsConfig pillEffectsConfig)
        {
            _pillFactory = pillFactory;

            _maxPills = pillSpawnerConfig.MaxPills;

            _minDistance = pillSpawnerConfig.MinDistance;
            _maxDistance = pillSpawnerConfig.MaxDistance;

            _gameObjectDestroyerService = gameObjectDestroyerService;

            _playerTransform = playerTransform;

            _pillEffectsConfig = pillEffectsConfig;

            CreateEffectsAndInitMap();
            HandlePillDestroy(null);
        }

        public void OnEnable()
        {
            _gameObjectDestroyerService.Destroyed += HandlePillDestroy;
        }

        public void OnDisable()
        {
            _gameObjectDestroyerService.Destroyed -= HandlePillDestroy;
        }

        private void HandlePillDestroy(GameObject gameobject)
        {
            if (gameobject != null && gameobject.TryGetComponent<PillView>(out _))
                _currentPills--;

            while (_currentPills < _maxPills)
            {
                SpawnPill();
            }
        }

        private void SpawnPill()
        {
            var pillsCount = Enum.GetValues(typeof(PillColorType)).Length;

            var rndPill = Random.Range(0, pillsCount);

            var colorType = (PillColorType) rndPill;

            Vector2 position =
                RandomPositionHandler.GetRandomPosition(_playerTransform.position, _minDistance, _maxDistance);

            _pillFactory.Create(_pillEffects[colorType], colorType, position, Quaternion.identity);
            _currentPills++;
        }


        private void CreateEffectsAndInitMap()
        {
            var pillDecreaseFireRateEffect = new PillDecreaseFireRateEffect(
                _pillEffectsConfig.PillDecreaseFireRateEffectConfig.Value,
                _pillEffectsConfig.PillDecreaseFireRateEffectConfig.DurationSec);

            var pillIncreaseSpeedEffect = new PillIncreaseSpeedEffect(
                _pillEffectsConfig.PillIncreaseSpeedEffectConfig.Value,
                _pillEffectsConfig.PillIncreaseSpeedEffectConfig.DurationSec);

            var pillHealHpEffect = new PillHealHpEffect(_pillEffectsConfig.PillHealHpEffectConfig.Value);

            var pillShockWaveEffect = new PillShockWaveEffect(_pillEffectsConfig.PillShockWaveEffectConfig.Radius,
                _pillEffectsConfig.PillShockWaveEffectConfig.DamageValue);

            _pillEffects[PillColorType.Yellow] = new List<IPillEffect> {pillDecreaseFireRateEffect};
            _pillEffects[PillColorType.Blue] = new List<IPillEffect> {pillIncreaseSpeedEffect};
            _pillEffects[PillColorType.Green] = new List<IPillEffect> {pillHealHpEffect};
            _pillEffects[PillColorType.Red] = new List<IPillEffect> {pillShockWaveEffect};
        }
    }
}