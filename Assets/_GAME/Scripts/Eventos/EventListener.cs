using MoreMountains.Feedbacks;
using System;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    [SerializeField] private MMF_Player _introCañon;

    private void OnEnable()
    {
        CañonController.OnCañonDisparado += IntroCañon;
    }

    private void OnDisable()
    {
        CañonController.OnCañonDisparado -= IntroCañon;
    }
    private void IntroCañon(Vector2 vector, float arg2)
    {
        _introCañon.PlayFeedbacks();
    }
}
