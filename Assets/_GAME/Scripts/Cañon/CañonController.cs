using UnityEngine;
using UnityEngine.InputSystem;

public class CañonController : MonoBehaviour
{
    enum EstadoCañon
    {
        NONE,
        APUNTANDO,
        PREPARANDO_DISPARO
    }

    //Posicion del mouse en relacion al mundo del juego
    private Vector2 _mousePos;
    [SerializeField] private EstadoCañon estadoCañon;

    [SerializeField] private float _maxAngle;
    [SerializeField] private float _minAngle;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _cañonPivote;

    private void OnEnable()
    {
        InputManager.GameInputs.Cañon.Mouse.performed += OnMouseMove;
        InputManager.GameInputs.Cañon.Accion.performed += OnAction;
    }

    private void OnDisable()
    {
        InputManager.GameInputs.Cañon.Mouse.performed -= OnMouseMove;
        InputManager.GameInputs.Cañon.Accion.performed -= OnMouseMove;
    }

    private void OnAction(InputAction.CallbackContext context)
    {

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

    }

    private void MoverCañon()
    {
        Vector3 dir = (Vector3)_mousePos - _cañonPivote.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        angle = Mathf.Clamp(angle, _minAngle, _maxAngle);

        float actualAngle = _cañonPivote.localEulerAngles.z;

        // Convertir de 0-360 a -180-180
        if (actualAngle > 180f) actualAngle -= 360f;

        float finalAngle = Mathf.LerpAngle(actualAngle, angle, _rotationSpeed * Time.deltaTime);

        _cañonPivote.localRotation = Quaternion.Euler(0,0, finalAngle);
    }
}
