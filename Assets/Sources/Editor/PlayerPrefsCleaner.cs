using UnityEditor;
using UnityEngine;

public sealed class PlayerPrefsCleaner : Editor
{
    [MenuItem("CustomEditor/Clear Prefs")]
    private static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        EditorPrefs.DeleteAll();
    }
}