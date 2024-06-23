using UnityEditor;
using UnityEngine;

public sealed class PrefsCleaner : Editor
{
    [MenuItem("CustomEditor/Clear Prefs")]
    private static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        EditorPrefs.DeleteAll();
    }
}