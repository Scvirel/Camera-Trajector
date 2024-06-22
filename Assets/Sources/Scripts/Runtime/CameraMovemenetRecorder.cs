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

        [Range(1,60)]
        [SerializeField] private int _timeoutSec = 3;

        private Transform _cameraTransform;

        private List<XYZDto> _locations = new List<XYZDto>();
        private List<XYZDto> _rotations = new List<XYZDto>();

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

            if (_locations.Count > 0 || _rotations.Count > 0)
            {
                _locations.Clear();
                _rotations.Clear();
            }

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

            PlayerPrefs.SetString(Paths.RecordingsDataPrefs, JsonUtility.ToJson(_recordings.Value));
            PlayerPrefs.Save();
        }

        private IEnumerator TransformRecording()
        {
            while (true)
            {
                Debug.Log($"Saved Location : ({_cameraTransform.position.x},{_cameraTransform.position.y},{_cameraTransform.position.z})");
                Debug.Log($"Saved Rotation : ({_cameraTransform.eulerAngles.x},{_cameraTransform.eulerAngles.y},{_cameraTransform.eulerAngles.z})");

                _locations.Add(
                    new XYZDto(
                        _cameraTransform.position.x,
                        _cameraTransform.position.y,
                        _cameraTransform.position.z
                        )
                    );

                _rotations.Add(
                  new XYZDto(
                      _cameraTransform.eulerAngles.x,
                      _cameraTransform.eulerAngles.y,
                      _cameraTransform.eulerAngles.z
                      )
                  );

                yield return _waiter;
            }
        }
    }
}