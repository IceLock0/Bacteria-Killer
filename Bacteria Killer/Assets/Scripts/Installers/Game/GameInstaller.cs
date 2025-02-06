using Services.Audio;
using Services.SaveLoad;
using Utils.ResourcesPathes.Utils;
using Zenject;

namespace Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSaveLoadService();
            
            BindAudioService();
        }

        private void BindSaveLoadService()
        {
            Container
                .Bind<ISaveLoadService>()
                .To<SaveLoadService>()
                .AsSingle();
        }

        private void BindAudioService()
        {
            Container
                .Bind<IAudioService>()
                .To<AudioService>()
                .FromComponentInNewPrefabResource(UtilsResourcesPathProvider.AUDIO_SERVICE)
                .AsSingle();
        }
    }
}