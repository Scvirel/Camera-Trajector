namespace CameraTrajector.Client
{
    public sealed class TrajectoryRepeatSignal
    {
        public TrajectoryModel Trajectory { get; }

        public TrajectoryRepeatSignal(TrajectoryModel trajectory)
        {
            Trajectory = trajectory;
        }
    }
}