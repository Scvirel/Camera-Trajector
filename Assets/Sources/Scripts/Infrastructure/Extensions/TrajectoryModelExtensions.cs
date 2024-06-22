using UnityEngine;

namespace CameraTrajector.Client
{
    public static class TrajectoryModelExtensions
    {
        public static Vector3 GetLocation(this TrajectoryModel model, int index)
        {
            return new Vector3(
                model.Locations[index].X,
                model.Locations[index].Y,
                model.Locations[index].Z
                );
        }

        public static Quaternion GetRotation(this TrajectoryModel model, int index)
        {
            return Quaternion.Euler(
                model.Rotations[index].X,
                model.Rotations[index].Y,
                model.Rotations[index].Z
                );
        }
    }
}