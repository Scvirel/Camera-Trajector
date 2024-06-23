using Zenject;

namespace CameraTrajector.Client
{
    public sealed class RepeaterBehaviourInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .DeclareSignal<TrajectoryRepeatSignal>();
        }
    }
}