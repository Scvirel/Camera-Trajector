using Zenject;

namespace CameraTrajector.Client
{
    public sealed class SignalBusInjector : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
        }
    }
}