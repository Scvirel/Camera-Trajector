using System;
using System.Collections.Generic;

namespace CameraTrajector.Client
{
    [Serializable]
    public sealed class MovementTrajectoryData
    {
        public List<TrajectoryModel> TrajectoryModels = new List<TrajectoryModel>();

        public MovementTrajectoryData(List<TrajectoryModel> trajectoryModels)
        {
            TrajectoryModels = trajectoryModels;
        }
    }
}