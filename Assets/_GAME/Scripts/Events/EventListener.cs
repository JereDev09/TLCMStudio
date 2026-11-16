using MoreMountains.Feedbacks;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    [SerializeField] private MMF_Player _introCañon;

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
        _introCañon.PlayFeedbacks();
    }
}
