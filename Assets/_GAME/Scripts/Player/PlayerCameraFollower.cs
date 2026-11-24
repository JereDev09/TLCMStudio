using System;
using UnityEngine;

public class PlayerCameraFollower : MonoBehaviour
{
    private Transform _target;

    private void OnEnable()
    {
        CañonController.OnPlayerCreated += SetCameraTarget;
    }
    private void OnDisable()
    {
        CañonController.OnPlayerCreated -= SetCameraTarget;
    }

    private void SetCameraTarget(PlayerController controller)
    {
        _target = controller.PlayerTransform;
    }

    private void Update()
    {
        if (_target != null)
        {
            transform.position = _target.position;
        }
    }
}
