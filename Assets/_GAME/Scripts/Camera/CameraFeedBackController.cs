using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFeedBackController : MonoBehaviour
{
    private Camera _myCamera;

    [Header("Camera Shake Config")]
    [SerializeField] private float shakeMultiplier;
    [SerializeField] private float maxShakeAmount;
    [SerializeField] private float maxShakeSpeed;
    [SerializeField] private float minShakeFrenquency;
    [SerializeField] private float ShakeAmountMultiplier;
    [SerializeField] private float ShakeSpeedMultiplier;
    [SerializeField] private float ShakeFrequencyFactor;

    private Vector2 _shakePos;
    private Vector2 _targetShake;
    private Vector3 _cameraOffset = new Vector3(0, 0, -10f);
    private Vector2 _cameraPos;
    private float _shakeAmount;
    private float _shakeSpeed;
    private float _shakeFrequency;
    private float _shakeTime;


    [Header("transform config")]
    [SerializeField] private Transform _defaultTarget;
    [SerializeField] private float _defaultZoom;
    [SerializeField] private AnimationCurve _defaultMovementCurve;
    private AnimationCurve _animationCurve;
    private Transform _target;
    private Vector3 _oldPos;
    private float _oldZoom;
    private float _cameraZoom;
    private float _zoomTarget;
    private float _transformDuration;
    private float _transformTime;
    private DeltaTimeType _transformUpdateType;

    public static event Action OnTransformFinished;

    private void Start()
    {
        _myCamera = gameObject.GetComponent<Camera>();
        _cameraPos = transform.position;
        ResetTarget(0, DeltaTimeType.NORMAL);
    }

    private void Update()
    {
        ShakeController();

        MovementController();

        transform.position = (Vector3)_cameraPos + _cameraOffset + (Vector3)_shakePos;

        _myCamera.orthographicSize = _cameraZoom;
    }

    private void ShakeController()
    {
        _shakeAmount = Mathf.Min(GameManager.WorldSpeed * ShakeAmountMultiplier,maxShakeAmount);
        _shakeSpeed = Mathf.Min(GameManager.WorldSpeed * ShakeSpeedMultiplier, maxShakeSpeed);
        _shakeFrequency = Mathf.Max(ShakeFrequencyFactor / GameManager.WorldSpeed,minShakeFrenquency);

        _shakeTime += Time.deltaTime;

        if (_shakeTime > _shakeFrequency)
        {
            _shakeTime = 0;
            _targetShake = new Vector2(UnityEngine.Random.Range(-_shakeAmount, _shakeAmount), UnityEngine.Random.Range(-_shakeAmount, _shakeAmount));
        }

        _shakePos = Vector2.Lerp(_shakePos, _targetShake, _shakeSpeed * Time.deltaTime) * shakeMultiplier;
    }

    private void MovementController()
    {
        float myDeltaTime = _transformUpdateType == DeltaTimeType.NORMAL ? Time.deltaTime : Time.unscaledDeltaTime;
        
        if (_transformDuration != 0)
        {
            //hace que ese evento solo se llame una vez al estar aislado

            if (_transformTime < 1)
            {
                _transformTime += myDeltaTime / _transformDuration;

                if (_transformTime >= 1)
                {
                    OnTransformFinished?.Invoke();
                }
            }

            _transformTime = Mathf.Clamp01(_transformTime);
        }

        Vector2 newPos = Vector2.LerpUnclamped(_oldPos, (Vector2)_target.position, _animationCurve.Evaluate(_transformTime));
        float newZoom = Mathf.LerpUnclamped(_oldZoom, _zoomTarget, _animationCurve.Evaluate(_transformTime));
        _cameraZoom = newZoom;

        _cameraPos = newPos;
    }

    public void SetTarget(Transform target,float duration,float zoom,AnimationCurve curve,DeltaTimeType transformType)
    {
        if (duration < 0)
        {
            Debug.LogWarning("no pueden haber duracion negativa (usando su valor positivo)");
            duration = Mathf.Abs(duration);
        }

        _oldPos = transform.position;
        _oldZoom = _myCamera.orthographicSize;
        _target = target;
        _zoomTarget = zoom;
        _transformDuration = duration;
        _transformUpdateType = transformType;
        _animationCurve = curve;

        if (duration != 0f)
        {
            _transformTime =  0f;
        }

        else
        {
            _transformTime = 1f;

            OnTransformFinished?.Invoke();
        }
    }

    public void ResetTarget(float duration,DeltaTimeType transformType)
    {
        SetTarget(_defaultTarget, duration, _defaultZoom,_defaultMovementCurve, transformType);
    }
}
