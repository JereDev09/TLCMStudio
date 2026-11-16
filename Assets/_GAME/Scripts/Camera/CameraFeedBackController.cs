using System;
using UnityEngine;

public class CameraFeedBackController : MonoBehaviour
{
    private Camera _myCamera;
    public static CameraFeedBackController Instance;

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
    [SerializeField] private float _defaultDuration;
    private Vector3 _oldPos;
    private float _oldZoom;
    private float _transformTime;
    private bool _terminoEvento;

    private static CameraEvent cameraEventActive;

    public static event Action OnTransformFinished;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _myCamera = Camera.main;
        _cameraPos = transform.position;
    }

    private void Update()
    {
        ShakeController();

        EventController();

        _myCamera.transform.position = (Vector3)_cameraPos + _cameraOffset + (Vector3)_shakePos;
    }

    private void OnEnable()
    {
        OnTransformFinished += ResetTarget;
    }

    private void OnDisable()
    {
        OnTransformFinished -= ResetTarget;
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

    private void EventController()
    {
        if (cameraEventActive == null) return;

        if (!_terminoEvento)
        {
            float myDeltaTime = cameraEventActive.updateType == DeltaTimeType.NORMAL ? Time.deltaTime : Time.unscaledDeltaTime;

            if (cameraEventActive.duracion != 0)
            {
                _transformTime += myDeltaTime / cameraEventActive.duracion;

                _transformTime = Mathf.Clamp01(_transformTime);
            }
            else
            {
                _transformTime = 1f;
            }

            Vector2 newPos = Vector2.LerpUnclamped(_oldPos, (Vector2)cameraEventActive.target.position, cameraEventActive.curva.Evaluate(_transformTime));
            float newZoom = Mathf.LerpUnclamped(_oldZoom, cameraEventActive.zoom, cameraEventActive.curva.Evaluate(_transformTime));

            _myCamera.orthographicSize = newZoom;

            _cameraPos = newPos;

            if (_transformTime >= 1)
            {
                _terminoEvento = true;

                OnTransformFinished?.Invoke();
            }
        }
    }

    public void SetTarget(CameraEvent cameraEvent)
    {
        if (cameraEvent.duracion < 0)
        {
            Debug.LogWarning("no pueden haber duracion negativa (usando su valor positivo)");
            cameraEvent.duracion = Mathf.Abs(cameraEvent.duracion);
        }

        cameraEventActive = cameraEvent;

        _oldPos = _cameraPos;
        _oldZoom = _myCamera.orthographicSize;

        _transformTime = 0f;
        _terminoEvento = false;
    }

    private void ResetTarget()
    {
        _terminoEvento = true;
        _transformTime = 0f;

        if (cameraEventActive != null && cameraEventActive.resetearAlTerminar)
        {
            CameraEvent cameraEvent = new(false,_defaultTarget,_defaultMovementCurve,DeltaTimeType.NORMAL,_defaultZoom, _defaultDuration);
            SetTarget(cameraEvent);

            return;
        }

        cameraEventActive = null;
    }
}
