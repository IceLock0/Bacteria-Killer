using Services.SaveLoad;
using Zenject;

namespace Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSaveLoadService();
        }

        private void BindSaveLoadService()
        {
            Container
                .Bind<ISaveLoadService>()
                .To<SaveLoadService>()
                .AsSingle();
        }
    }
}