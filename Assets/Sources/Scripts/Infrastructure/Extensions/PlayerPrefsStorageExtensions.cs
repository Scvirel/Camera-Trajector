using System;
using UnityEngine;

namespace CameraTrajector.Client
{
    public static class PlayerPrefsStorageExtensions
    {
        public static void ToJsonPrefs<Type>(this Type obj, string path)
        {
            PlayerPrefs.SetString(Paths.RecordingsDataPrefs, JsonUtility.ToJson(obj));
            PlayerPrefs.Save();
        }

        public static void FromPrefsJson<Type>(this Type obj, string path)
        {
            if (PlayerPrefs.HasKey(path))
            {
                try
                {
                    JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(path), obj);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }
    }
}