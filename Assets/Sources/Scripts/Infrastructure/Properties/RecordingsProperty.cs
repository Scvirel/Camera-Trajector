using System.Collections.Generic;

namespace CameraTrajector.Client
{
    public sealed class RecordingsProperty : IRecordings
    {
        private MovementTrajectoryData _value = new MovementTrajectoryData(new List<TrajectoryModel>());

        public MovementTrajectoryData Value 
        { 
            get => _value; 
            set => _value = value;
        }
    }
}