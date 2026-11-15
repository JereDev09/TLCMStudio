using System;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _introCañon;

    private void OnEnable()
    {
        CañonController.OnCañonDisparado += TransicionCamaraInicial;
    }

    private void OnDisable()
    {
        CañonController.OnCañonDisparado -= TransicionCamaraInicial;
    }

    private void TransicionCamaraInicial(Vector2 vector, float arg2)
    {
        EventManager.StartEvent(_introCañon);
    }
}
