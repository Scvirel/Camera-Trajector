using Zenject;

namespace CameraTrajector.Client
{
    public sealed class RepeatBehaviourInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container
                .DeclareSignal<TrajectoryRepeatSignal>();
        }
    }
}