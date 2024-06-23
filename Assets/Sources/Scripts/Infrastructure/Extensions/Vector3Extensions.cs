using System;
using UnityEngine;

namespace CameraTrajector.Client
{
    public static class Vector3Extensions
    {
        public static Type SimplifyTo<Type>(this Vector3 vector)
        {
            return (Type)Activator.CreateInstance(typeof(Type), vector.x, vector.y, vector.z);
        }
    }
}