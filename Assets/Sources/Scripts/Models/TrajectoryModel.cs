using System;
using System.Collections.Generic;

namespace CameraTrajector.Client
{
    [Serializable]
    public sealed class TrajectoryModel
    {
        public string Id;
        public int TimeoutSec;
        public List<XYZDto> Locations;
        public List<XYZDto> Rotations;

        public TrajectoryModel(string id, int timeoutSec,  List<XYZDto> locations, List<XYZDto> rotations)
        {
            Id = id;
            TimeoutSec = timeoutSec;
            Locations = locations;
            Rotations = rotations;
        }
    }
}