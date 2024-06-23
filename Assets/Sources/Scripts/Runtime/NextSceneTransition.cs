using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CameraTrajector.Client
{
    public sealed class NextSceneTransition : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void SceneTransition()
        {
            StartCoroutine(Transition());
        }

        private IEnumerator Transition()
        {
            //TODO:Probaly show any loading screen here

            AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
            yield return new WaitUntil(() => operation.isDone);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneName));

            operation = SceneManager.UnloadSceneAsync(gameObject.scene);
            yield return new WaitUntil(() => operation.isDone);
        }
    }
}