using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    private Vector3 _startPos;
    [SerializeField] private float _amount;

    private void Start()
    {
        _startPos = transform.position;
    }

    private void LateUpdate()
    {
        Vector3 offset = new Vector3(_camera.position.x * _amount, 0f,0f);

        transform.position = _startPos + offset;
    }
}
