using UnityEngine;

namespace CameraTrajector.Client
{
    public sealed class ExitGame : MonoBehaviour
    {
        public void GameExit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}