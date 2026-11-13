using System;
using UnityEngine;

public class CameraEventListener : MonoBehaviour
{
    [SerializeField] private CameraFeedBackController _feedBackController;

    [Header("configuracion evento cañon")]
    [SerializeField] private Transform _targetEventoCañon;
    [SerializeField] private AnimationCurve _curvaEventoCañon;
    [SerializeField] private float _zoomEventoCañon;
    [SerializeField] private float _duracionEventoCañon;

    private void OnEnable()
    {
        CañonController.OnCañonDisparado += TransicionCamaraInicial;
        CameraFeedBackController.OnTransformFinished += FinalizoTransformacion;
    }

    private void FinalizoTransformacion()
    {
        Time.timeScale = 1f;

        _feedBackController.ResetTarget(.25f, DeltaTimeType.NORMAL);
    }

    private void OnDisable()
    {
        CañonController.OnCañonDisparado -= TransicionCamaraInicial;
        CameraFeedBackController.OnTransformFinished -= FinalizoTransformacion;
    }

    private void TransicionCamaraInicial(Vector2 vector, float arg2)
    {
        Time.timeScale = 0f;

        _feedBackController.SetTarget(_targetEventoCañon, _duracionEventoCañon, _zoomEventoCañon, _curvaEventoCañon, DeltaTimeType.UNSCALE);
    }
}
