using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class CañonController : MonoBehaviour
{
    //Posicion del mouse en relacion al mundo del juego
    private Vector2 _mousePos;

    [SerializeField] private Vector3 _cañonDir;

    [Header("Configuracion velocidadesPosibles")]

    [SerializeField] private float _minVelocityCañon;
    [Tooltip("velocidad maxima que dara el cañon al darle perfecto")]
    [SerializeField] private float _maxVelocityCañon;

    [Tooltip("rango en el cual se considerara un tiro como perfecto")]
    [Range(0f,1f)]
    [SerializeField] private float _toleranciaPerfecto;


    [Header("configuracion cañon")]

    [SerializeField] private EstadoCañon estadoCañon;

    [SerializeField] private float _maxAngle;
    [SerializeField] private float _minAngle;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _cañonPivote;
    [SerializeField] private float _potenciaDisparo;
    [SerializeField] private float _velocidadOscilacionPotencia;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerOrigin;
    [SerializeField] private float _playerXOrigin;
    private float _tiempoPreDisparo;
    public static event Action<EstadoCañon> OnCambioEstadoCañon;
    public static event Action<Vector2,float> OnCañonDisparado;
    public static event Action<PlayerController> OnPlayerCreated;

    private void OnEnable()
    {
        InputManager.GameInputs.Cañon.Mouse.performed += OnMouseMove;
        InputManager.GameInputs.Cañon.Accion.performed += OnAction;

        OnCambioEstadoCañon += ReseteoEstadoCañon;
    }

    private void OnDisable()
    {
        InputManager.GameInputs.Cañon.Mouse.performed -= OnMouseMove;
        InputManager.GameInputs.Cañon.Accion.performed -= OnMouseMove;

        OnCambioEstadoCañon -= ReseteoEstadoCañon;
    }

    private void ReseteoEstadoCañon(EstadoCañon estadoCañon)
    {
        switch (estadoCañon)
        {
            case EstadoCañon.NONE:
                break;
            case EstadoCañon.APUNTANDO:
                break;
            case EstadoCañon.PREPARANDO_DISPARO:
                _potenciaDisparo = UnityEngine.Random.Range(-1f, 1f);
                break;
            case EstadoCañon.DISPARANDO:
                break;
            default:
                break;
        }
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        switch (estadoCañon)
        {
            case EstadoCañon.NONE:
                break;
            case EstadoCañon.APUNTANDO:

                estadoCañon = EstadoCañon.PREPARANDO_DISPARO;
                OnCambioEstadoCañon?.Invoke(estadoCañon);
                break;
            case EstadoCañon.PREPARANDO_DISPARO:
                estadoCañon = EstadoCañon.DISPARANDO;
                OnCambioEstadoCañon?.Invoke(estadoCañon);
                OnCañonDisparado?.Invoke(_cañonDir,_potenciaDisparo);
                break;
            case EstadoCañon.DISPARANDO:
                
                break;
            default:
                break;
        }
    }

    public void StartGame()
    {
        float speed = Mathf.Lerp(_maxVelocityCañon, _minVelocityCañon, Mathf.Abs(_potenciaDisparo));

        GameManager.SetWorldSpeed(speed);

        if (_playerPrefab != null)
        {
            PlayerController player = Instantiate(_playerPrefab, new Vector2(_playerXOrigin, _playerOrigin.transform.position.y), Quaternion.identity).GetComponent<PlayerController>();

            player.SetIntialVelocity(speed, _cañonDir);

            OnPlayerCreated?.Invoke(player);

        }

        GameManager.StartGame();
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        _mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    private void Update()
    {
        if (_cañonPivote == null)
        {
            Debug.LogWarning("no se asigno pivote al cañon");
            return;
        }

        CañonStateController();
    }

    private void CañonStateController()
    {
        switch (estadoCañon)
        {
            case EstadoCañon.NONE:
                break;
            case EstadoCañon.APUNTANDO:
                MoverCañon();
                break;
            case EstadoCañon.PREPARANDO_DISPARO:
                PrepararDisparo();
                break;
            default:
                break;
        }
    }

    private void PrepararDisparo()
    {
        _tiempoPreDisparo += Time.deltaTime * _velocidadOscilacionPotencia;

        _potenciaDisparo = Mathf.Cos(_tiempoPreDisparo);
    }

    private void MoverCañon()
    {
        _cañonDir = ((Vector3)_mousePos - _cañonPivote.position).normalized;

        float angle = Mathf.Atan2(_cañonDir.y, _cañonDir.x) * Mathf.Rad2Deg;

        angle = Mathf.Clamp(angle, _minAngle, _maxAngle);

        float actualAngle = _cañonPivote.localEulerAngles.z;

        // Convertir de 0-360 a -180-180
        if (actualAngle > 180f) actualAngle -= 360f;

        float finalAngle = Mathf.LerpAngle(actualAngle, angle, _rotationSpeed * Time.deltaTime);

        _cañonPivote.localRotation = Quaternion.Euler(0,0, finalAngle);
    }

    public float PotenciaDisparo => _potenciaDisparo;
}
