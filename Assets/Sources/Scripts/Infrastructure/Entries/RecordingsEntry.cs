using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CameraTrajector.Client
{
    public sealed class RecordingsEntry : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus = default;

        public class Factory : PlaceholderFactory<RecordingsEntry>
        { }

        [SerializeField] private TextMeshProUGUI _idField;
        [SerializeField] private Image _selectableImage;

        private TrajectoryModel _metaTrajectory;

        public void Construct(TrajectoryModel model)
        {
            _metaTrajectory = model;

            _idField.text = model.Id;
        }

        public void PlayRecording()
        {
            _signalBus.Fire(new TrajectoryRepeatSignal(_metaTrajectory));
        }
    }
}