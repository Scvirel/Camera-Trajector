using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CameraTrajector.Client
{
    public sealed class GameBootstraper : MonoBehaviour
    {
        [Inject] private readonly IRecordings _recordings = default;

        private void Start()
        {
            StartCoroutine(LoadDefaultScenes());

            if (PlayerPrefs.HasKey(Paths.RecordingsDataPrefs))
            {
                string str = PlayerPrefs.GetString(Paths.RecordingsDataPrefs);

                _recordings.Value =
                    JsonUtility.FromJson<MovementTrajectoryData>(
                        PlayerPrefs.GetString(Paths.RecordingsDataPrefs)
                        );
            }
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