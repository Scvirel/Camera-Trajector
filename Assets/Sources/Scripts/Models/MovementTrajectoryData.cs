using System;
using System.Collections.Generic;

namespace CameraTrajector.Client
{
    [Serializable]
    public sealed class MovementTrajectoryData
    {
        public List<TrajectoryModel> TrajectoryModels;

        public MovementTrajectoryData(List<TrajectoryModel> trajectoryModels)
        {
            TrajectoryModels = trajectoryModels;
        }
    }
}