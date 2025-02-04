using Services.Destroyer;
using Services.Detector;
using Services.Enemy;
using Services.Fabric.EnemyFabric;
using Services.Fabric.PlayerFabric;
using Services.Finder;
using Services.Input;
using Services.Level;
using Services.Movement.PositionProvider;
using Services.SaveLoad;
using Services.Updater;
using Services.Upgrade;
using Utils.Factory.PillFactory;
using Utils.Factory.UI;
using Zenject;

namespace Installers.Scene.Gameplay.Services
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInputService();
            
            BindClosetObjectDetectorService();
            
            BindEnemyFactory();
            
            BindPlayerFactory();
            
            BindPlayerTranformProviderService();
            
            BindUpdaterService();
            
            BindGameObjectDestroyerService();
            
            BindPillsFactory();
            
            BindPlayerUnspentLevelsProviderService();
            
            BindUIFactory();
            
            BindUpgradeViewFactory();
            
            BindPlayerUpgradeProviderService();
            
            BindEnemyTransformsProviderService();
        }

        private void BindEnemyTransformsProviderService()
        {
            Container
                .Bind<IEnemyTransformsProviderService>()
                .To<EnemyTransformsProviderService>()
                .AsSingle();
        }

        private void BindPlayerUpgradeProviderService()
        {
            Container
                .Bind<IPlayerUpgradeProviderService>()
                .To<PlayerUpgradeProviderService>()
                .AsSingle();
        }

        private void BindUpgradeViewFactory()
        {
            Container
                .Bind<IUpgradeViewFactory>()
                .To<UpgradeViewFactory>()
                .AsSingle();
        }
        
        private void BindUIFactory()
        {
            Container
                .Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle();
        }

        private void BindPlayerUnspentLevelsProviderService()
        {
            Container
                .Bind<IPlayerUnspentLevelsProviderService>()
                .To<PlayerUnspentLevelsProviderService>()
                .AsSingle();
        }

        private void BindPillsFactory()
        {
            Container
                .Bind<IPillFactory>()
                .To<PillFactory>()
                .AsSingle();
        }

        private void BindInputService()
        {
            Container
                .BindInterfacesAndSelfTo<InputService>()
                .AsSingle()
                .WithArguments(new InputController())
                .NonLazy();
        }

        private void BindClosetObjectDetectorService()
        {
            Container
                .Bind<IClosestObjectFindService>()
                .To<ClosestObjectFindService>()
                .AsSingle();
        }

        private void BindEnemyFactory()
        {
            Container
                .Bind<IEnemyFactory>()
                .To<EnemyFactory>()
                .AsSingle();
        }

        private void BindPlayerFactory()
        {
            Container
                .Bind<IPlayerFactory>()
                .To<PlayerFactory>()
                .AsSingle();
        }

        private void BindPlayerTranformProviderService()
        {
            Container
                .Bind<IPlayerTransformProviderService>()
                .To<PlayerTransformProviderService>()
                .AsSingle();
        }

        private void BindUpdaterService()
        {
            Container
                .Bind<IUpdaterService>()
                .To<UpdaterService>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }

        private void BindGameObjectDestroyerService()
        {
            Container
                .Bind<IGameObjectDestroyerService>()
                .To<GameObjectDestroyerService>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }
    }
}