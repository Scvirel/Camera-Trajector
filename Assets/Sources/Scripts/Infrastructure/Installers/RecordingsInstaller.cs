using Zenject;

namespace CameraTrajector.Client
{
    public sealed class RecordingsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IRecordings>()
                .To<RecordingsProperty>()
                .AsSingle();
        }
    }
}