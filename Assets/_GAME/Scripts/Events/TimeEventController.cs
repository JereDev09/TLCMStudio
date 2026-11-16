using System;
using UnityEngine;

public class TimeEventController : MonoBehaviour
{
    /// <summary>
    /// se usa en caso de que el evento sea marcado como <see cref="TimeEvent.resetearAlTerminar"/>
    /// </summary>
    [Tooltip("se usa en caso de que el evento sea marcado como resetearAlTerminar")]
    [SerializeField] private float _resetDuration;
    [SerializeField] private AnimationCurve _curvaReset;

    private static TimeEvent timeEventActive;
    private static TimeEventController Instance;
    private float _timeEvento;
    private static float _oldTimeScale;
    private bool _terminoEvento;
    public static event Action OnEventFinished;
    public static event Action OnEventStarted;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (timeEventActive == null) return;

        EventController();
    }

    private void OnEnable()
    {
        OnEventFinished += ResetEvent;
    }

    private void OnDisable()
    {
        OnEventFinished -= ResetEvent;
    }

    private void EventController()
    {
        if (!_terminoEvento)
        {
            if (timeEventActive.duracion != 0)
            {
                _timeEvento += Time.unscaledDeltaTime / timeEventActive.duracion;

                _timeEvento = Mathf.Clamp01(_timeEvento);
            }
            else
            {
                _timeEvento = 1f;
            }

            float newTimeScale = Mathf.LerpUnclamped(_oldTimeScale, timeEventActive.targetTimeScale, timeEventActive.curva.Evaluate(_timeEvento));

            Time.timeScale = newTimeScale;

            if (_timeEvento >= 1f)
            {
                _terminoEvento = true;

                OnEventFinished?.Invoke();
            }
        }
    }

    private void ResetEvent()
    {
        _terminoEvento = true;
        _timeEvento = 0f;

        if (timeEventActive.resetearAlTerminar)
        {
            TimeEvent timeEvent = new(false, 1f, _curvaReset, _resetDuration);

            StartEvent(timeEvent);

            return;
        }

        timeEventActive = null;
    }

    public static void StartEvent(TimeEvent timeEvent)
    {
        if (timeEvent.duracion < 0)
        {
            Debug.LogWarning("no pueden haber duracion negativa (usando su valor positivo)");
            timeEvent.duracion = Mathf.Abs(timeEvent.duracion);
        }

        timeEventActive = timeEvent;

        Instance._timeEvento = 0f;
        Instance._terminoEvento = false;

        _oldTimeScale = Time.timeScale;

        OnEventStarted?.Invoke();
    }
}
