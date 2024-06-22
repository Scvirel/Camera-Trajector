using System.Collections;
using UnityEngine;
using Zenject;

namespace CameraTrajector.Client
{
    public sealed class CameraTrajectoryRepeaterer : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus = default;

        private Transform _cameraTransform;
        private Coroutine _repeatingProcess;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;
            _signalBus.Subscribe<TrajectoryRepeatSignal>(Repeat);
        }
        private void OnDestroy()
        {
            _signalBus.Unsubscribe<TrajectoryRepeatSignal>(Repeat);
        }

        public void Repeat(TrajectoryRepeatSignal repeatSignal)
        {
            if (_repeatingProcess != null)
            {
                StopCoroutine(_repeatingProcess);
            }

            if (repeatSignal.Trajectory.Rotations.Count == repeatSignal.Trajectory.Locations.Count &&
                repeatSignal.Trajectory.Rotations.Count != 0)
            {
                _cameraTransform.position = repeatSignal.Trajectory.GetLocation(0);
                _cameraTransform.rotation = repeatSignal.Trajectory.GetRotation(0);

                if (repeatSignal.Trajectory.Rotations.Count > 1)
                {
                    _repeatingProcess = StartCoroutine(RepeatCameraTrajectory(repeatSignal.Trajectory));
                }
            }
        }

        private IEnumerator RepeatCameraTrajectory(TrajectoryModel trajectory)
        {
            int iterator = 1;

            while (iterator < trajectory.Rotations.Count)
            {
                Vector3 targetPosition = trajectory.GetLocation(iterator);
                Quaternion targetRotation = trajectory.GetRotation(iterator);

                float elapsedTime = 0f;
                Vector3 currentPosition = _cameraTransform.position;
                Quaternion currentRotation = _cameraTransform.rotation;

                while (elapsedTime < trajectory.TimeoutSec)
                {
                    _cameraTransform.position = Vector3.Lerp(currentPosition, targetPosition, elapsedTime / trajectory.TimeoutSec);
                    _cameraTransform.rotation = Quaternion.Lerp(currentRotation, targetRotation, elapsedTime / trajectory.TimeoutSec);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                iterator++;
            }
        }
    }
}