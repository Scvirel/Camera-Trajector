using System;
using UnityEngine;

namespace CameraTrajector.Client
{
    public sealed class CameraRotator : MonoBehaviour
    {
        [SerializeField]
        [Range(0.1f, 10f)]
        public float _sensitivity;

        [SerializeField]
        [Range(0.0f, 1f)]
        private float _slerp;

        private const float MinXRotAngle = -89;
        private const float MaxXRotAngle = 89;

#if UNITY_ANDROID || UNITY_IOS
        private Touch touch;
#endif

        private float _targetDistance;
        private Transform _cameraTransform;
        private Vector2 _directionDelta;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;
            _targetDistance = Vector3.Distance(_cameraTransform.position, Constants.TargetLocation);
        }

        private void Update()
        {
            HandleDirectionDelta();
        }
        private void LateUpdate()
        {
            ApplyDirectionDelta();
        }

        private void HandleDirectionDelta()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
            if (Input.GetMouseButton(0))
            {
                _directionDelta.x += -Input.GetAxis("Mouse Y") * _sensitivity;
                _directionDelta.y += Input.GetAxis("Mouse X") * _sensitivity;
            }

            if (_directionDelta.x < MinXRotAngle)
            {
                _directionDelta.x = MinXRotAngle;
            }
            else if (_directionDelta.x > MaxXRotAngle)
            {
                _directionDelta.x = MaxXRotAngle;
            }

#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                _directionDelta += touch.deltaPosition * Time.deltaTime * _sensitivity;
            }
        }

        if (_directionDelta.y < MinXRotAngle)
        {
            _directionDelta.y = MinXRotAngle;
        }
        else if (_directionDelta.y > MaxXRotAngle)
        {
            _directionDelta.y = MaxXRotAngle;
        }
#endif
        }
        private void ApplyDirectionDelta()
        {
            Vector3 direction = new Vector3(0, 0, -_targetDistance);

            Quaternion rotationDelta;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
            rotationDelta = Quaternion.Euler(_directionDelta.x, _directionDelta.y, 0);

#elif UNITY_ANDROID || UNITY_IOS
            rotationDelta = Quaternion.Euler(_directionDelta.y, -_directionDelta.x, 0);
#endif

            _cameraTransform.rotation = Quaternion.Slerp(_cameraTransform.rotation, rotationDelta, _slerp);
            _cameraTransform.position = _cameraTransform.rotation * direction;
            _cameraTransform.LookAt(Constants.TargetLocation);
        }
    }
}