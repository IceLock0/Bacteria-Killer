using Services.Destroyer;
using Services.Detector;
using Services.Fabric.EnemyFabric;
using Services.Fabric.PlayerFabric;
using Services.Finder;
using Services.Input;
using Services.Movement.PositionProvider;
using Services.Updater;
using Utils.Factory.PillFactory;
using Zenject;

namespace Installers.Scene.Gameplay.Services
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInputService();
            BindClosetObjectDetectorService();
            BindEnemyFactoryInstaller();
            BindPlayerFactoryInstaller();
            BindPlayerPositionProviderService();
            BindUpdaterService();
            BindGameObjectDestroyerService();
            BindPillsFactoryInstaller();
        }

        private void BindPillsFactoryInstaller()
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

        private void BindEnemyFactoryInstaller()
        {
            Container
                .Bind<IEnemyFactory>()
                .To<EnemyFactory>()
                .AsSingle();
        }

        private void BindPlayerFactoryInstaller()
        {
            Container
                .Bind<IPlayerFactory>()
                .To<PlayerFactory>()
                .AsSingle();
        }

        private void BindPlayerPositionProviderService()
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