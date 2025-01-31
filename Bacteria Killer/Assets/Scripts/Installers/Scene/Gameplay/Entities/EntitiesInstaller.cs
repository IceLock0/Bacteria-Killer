using View.Characters.Player;
using Zenject;

namespace Installers.Scene.Gameplay.Entities
{
    public class EntitiesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlayerView();
        }

        private void BindPlayerView()
        {
            Container
                .Bind<PlayerView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}