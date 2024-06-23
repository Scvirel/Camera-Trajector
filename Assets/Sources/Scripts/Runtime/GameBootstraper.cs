using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CameraTrajector.Client
{
    public sealed class GameBootstraper : MonoBehaviour
    {
        [Inject] private readonly IRecordings _recordings;

        [SerializeField] private int _targetFrameRate = 60;

        private void Start()
        {
            Application.targetFrameRate = _targetFrameRate;

            StartCoroutine(LoadDefaultScenes());

            _recordings.Value.FromPrefsJson(Paths.RecordingsDataPrefs);
        }

        private IEnumerator LoadDefaultScenes()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(Scenes.MainCamera, LoadSceneMode.Additive);
            yield return new WaitUntil(() => operation.isDone);

            operation = SceneManager.LoadSceneAsync(Scenes.SunLight, LoadSceneMode.Additive);
            yield return new WaitUntil(() => operation.isDone);

            operation = SceneManager.LoadSceneAsync(Scenes.EventSystem, LoadSceneMode.Additive);
            yield return new WaitUntil(() => operation.isDone);

            operation = SceneManager.LoadSceneAsync(Scenes.RecordingRoom, LoadSceneMode.Additive);
            yield return new WaitUntil(() => operation.isDone);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(Scenes.RecordingRoom));

            operation = SceneManager.UnloadSceneAsync(gameObject.scene);
            yield return new WaitUntil(() => operation.isDone);
        }
    }
}