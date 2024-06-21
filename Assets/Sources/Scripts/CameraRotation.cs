using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRotation : MonoBehaviour
{
    [SerializeField] 
    private Transform _target;

    [SerializeField]
    [Range(0.1f, 10f)]
    public float _sensitivity;

    [SerializeField]
    private float _slerp;

    private Vector2 _directionDelta;
    
    private Touch touch;
    private float _targetDistance;

    private const float MinXRotAngle = -89;
    private const float MaxXRotAngle = 89;

    private Transform _cameraTransform;

    private void Awake()
    {
        _cameraTransform = this.transform;
    }
    void Start()
    {
        _targetDistance = Vector3.Distance(transform.position, _target.position);
    }

    void Update()
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
        _cameraTransform.position = _target.position + _cameraTransform.rotation * direction;
        _cameraTransform.LookAt(_target.position);
    }
}