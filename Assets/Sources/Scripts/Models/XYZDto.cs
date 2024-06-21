using System;

namespace CameraTrajector.Client
{
    [Serializable]
    public sealed class XYZDto
    {
        public float X;
        public float Y;
        public float Z;

        public XYZDto(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}