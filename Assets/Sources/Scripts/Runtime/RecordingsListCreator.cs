using UnityEngine;
using Zenject;

namespace CameraTrajector.Client
{
    public sealed class RecordingsListCreator : MonoBehaviour
    {
        [Inject] private readonly IRecordings _recordings;
        [Inject] private readonly RecordingsEntry.Factory _factory;

        [SerializeField] private Transform _parent;

        private void Start()
        {
            CreateRecordings();
        }

        private void CreateRecordings()
        {
            foreach (TrajectoryModel model in _recordings.Value.TrajectoryModels)
            {
                RecordingsEntry tempRecording = _factory.Create();
                tempRecording.transform.SetParent(_parent);
                tempRecording.transform.localScale = Vector3.one;

                tempRecording.Construct(model);
            }
        }
    }
}