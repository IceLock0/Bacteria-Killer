using Entities.Player;
using Zenject;

namespace Installers.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlayerEnemyDetector();
        }

        private void BindPlayerEnemyDetector()
        {
            Container.Bind<PlayerClosestEnemyDetector>().AsSingle();
        }
    }
}