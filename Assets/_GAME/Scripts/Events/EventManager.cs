using System;
using UnityEngine;

[Serializable]
public class CameraEvent
{
    //solo se usa desde el inspector ya que ahi desde el GameEvent se definira si este evento surtira efecto
    public bool activo;
    public bool resetearAlTerminar;

    public Transform target;
    public AnimationCurve curva;
    public DeltaTimeType updateType;
    public float zoom;
    public float duracion;

    public CameraEvent(bool _resetearAlTerminar,Transform _target, AnimationCurve _curva, DeltaTimeType _updateType,float _zoom,float _duracion)
    {
        resetearAlTerminar = _resetearAlTerminar;
        target = _target;
        curva = _curva;
        updateType = _updateType;
        zoom = _zoom; 
        duracion = _duracion;
    }
}

[Serializable]
public class TimeEvent
{
    //solo se usa desde el inspector ya que ahi desde el GameEvent se definira si este evento surtira efecto
    public bool activo;
    public bool resetearAlTerminar;

    public float targetTimeScale;
    public AnimationCurve curva = AnimationCurve.Constant(0,1,1);

    /// <summary>
    /// por temas de logica este siempre usa el <see cref="Time.unscaledDeltaTime"/>
    /// </summary>
    public float duracion;

    public TimeEvent(bool resetearAlTerminar, float targetTimeScale, AnimationCurve curva, float duracion)
    {
        this.resetearAlTerminar = resetearAlTerminar;
        this.targetTimeScale = targetTimeScale;
        this.curva = curva;
        this.duracion = duracion;
    }
}

[Serializable]
public class GameEvent 
{
    /// <summary>
    /// sirve para saber que evento termino y cual es su identificador
    /// </summary>
    public string eventName;
    /// <summary>
    /// por temas de logica este siempre usa el <see cref="Time.unscaledDeltaTime"/>
    /// </summary>
    public float duracion;

    /// <summary>
    /// Define si el evento puede ser cancelado por otro
    /// </summary>
    public EventPriority prioridad;
    public CameraEvent cameraEvent;
    public TimeEvent timeEvent;
}

public class EventManager : MonoBehaviour
{
    private static GameEvent EventActive;
    private static EventManager Instance;
    private float _timeEvento;
    private bool _terminoEvento;

    /// <summary>
    /// Al terminar te pasa el nombre de ese evento como identificador para saber si es el que quieres
    /// </summary>
    public static event Action<string> OnEventFinished;
    public static event Action<string> OnEventStarted;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (EventActive == null) return;

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
            if (EventActive.duracion != 0)
            {
                _timeEvento += Time.unscaledDeltaTime;

                if (_timeEvento >= EventActive.duracion)
                {

                    OnEventFinished?.Invoke(EventActive.eventName);
                }
            }
            else
            {
                OnEventFinished?.Invoke(EventActive.eventName);
            }
        }
    }

    private void ResetEvent(string name)
    {
        _terminoEvento = true;
        _timeEvento = 0f;

        EventActive = null;
    }

    private static bool PuedeEjecutarEvento(EventPriority priority)
    {
        if (EventActive == null) return true;

        return (int)priority >= (int)EventActive.prioridad;
    }

    public static void StartEvent(GameEvent data)
    {
        if (data == null) return;

        if (!PuedeEjecutarEvento(data.prioridad)) 
        {
            Debug.LogWarning("Ya hay un evento de mayor prioridad en ejecucion");

            return; 
        }

        if (data.duracion < 0)
        {
            Debug.LogWarning("no pueden haber duracion negativa (usando su valor positivo)");
            data.duracion = Mathf.Abs(data.duracion);
        }

        Instance._timeEvento = 0;
        Instance._terminoEvento = false;

        if (data.cameraEvent.activo)
        {
            CameraFeedBackController.Instance.SetTarget(data.cameraEvent);
        }

        if (data.timeEvent.activo)
        {
            TimeEventController.StartEvent(data.timeEvent);
        }

        EventActive = data;

        OnEventStarted?.Invoke(data.eventName);
    }
}
