using Zenject;

namespace Installers.Services
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInputService();
            BindClosetObjectDetecrorService();
        }

        private void BindInputService()
        {
            Container.Bind<InputService>().AsSingle();
        }

        private void BindClosetObjectDetecrorService()
        {
            Container.Bind<ClosestObjectDetectorService>().AsSingle();
        }
    }
}