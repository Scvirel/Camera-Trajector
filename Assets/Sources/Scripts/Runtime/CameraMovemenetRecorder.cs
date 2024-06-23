using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CameraTrajector.Client
{
    public sealed class CameraMovemenetRecorder : MonoBehaviour
    {
        [Inject] private readonly IRecordings _recordings;

        [Range(1, 60)]
        [SerializeField] private int _timeoutSec = 1;

        private Transform _cameraTransform;

        private List<XYZDto> _locations;
        private List<XYZDto> _rotations;

        private WaitForSeconds _waiter;

        private Coroutine _recordingProcess;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;

            _waiter = new WaitForSeconds(_timeoutSec);
        }

        public void StartRecording()
        {
            Debug.Log("Recording started!");

            _locations = new List<XYZDto>();
            _rotations = new List<XYZDto>();

            _recordingProcess = StartCoroutine(TransformRecording());
        }
        public void CompleteRecording()
        {
            StopCoroutine(_recordingProcess);

            Debug.Log($"Recording completed!|_locations count :  {_locations.Count}|_rotations count :  {_rotations.Count}");

            _recordings.Value.TrajectoryModels.Add(
                new TrajectoryModel(
                    $"{Guid.NewGuid()}_{_recordings.Value.TrajectoryModels.Count}",
                    _timeoutSec,
                    _locations,
                    _rotations
                    )
                );

            _recordings.Value.ToJsonPrefs(Paths.RecordingsDataPrefs);
        }

        private IEnumerator TransformRecording()
        {
            while (true)
            {
                Debug.Log($"Saved Location : ({_cameraTransform.position.x},{_cameraTransform.position.y},{_cameraTransform.position.z})");
                Debug.Log($"Saved Rotation : ({_cameraTransform.eulerAngles.x},{_cameraTransform.eulerAngles.y},{_cameraTransform.eulerAngles.z})");

                _locations.Add(_cameraTransform.position.SimplifyTo<XYZDto>());
                _rotations.Add(_cameraTransform.eulerAngles.SimplifyTo<XYZDto>());

                yield return _waiter;
            }
        }
    }
}