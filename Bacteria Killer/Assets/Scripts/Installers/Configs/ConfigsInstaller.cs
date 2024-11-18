using Configs;
using UnityEngine;
using Zenject;

namespace Installers.Configs
{
    public class ConfigsInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        
        public override void InstallBindings()
        {
            BindPlayerConfig();
        }

        private void BindPlayerConfig()
        {
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle().NonLazy();
        }
    }
}