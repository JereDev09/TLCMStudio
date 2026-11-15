using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed;
    [SerializeField] private InputActionReference mouse;
    [SerializeField] private float _dinamicCamera;
    private Vector2 mousePos;

    private void Awake()
    {
        mouse.action.performed += OnMouse;
    }

    private void OnMouse(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }

    void Update()
    {
        if (_target == null) return;

        Vector3 targetPos = _target.position + _offset;

        Vector2 _mousePos = (Camera.main.ScreenToViewportPoint(mousePos) * 2f) - Vector3.one;

        Debug.Log(_mousePos);

        Vector3 dinamicPos = targetPos + ((Vector3)_mousePos * _dinamicCamera);

        transform.position = Vector3.Lerp(transform.position, dinamicPos, _speed * Time.deltaTime);

    }
}
