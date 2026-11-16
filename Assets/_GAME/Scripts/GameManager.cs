using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _intialWorldSpeed;
    [SerializeField] private float _intialMetrosAvanzados;

    private static float _worldSpeed;
    private static float _metrosAvanzados;

    private void Start()
    {
        _worldSpeed = _intialWorldSpeed;
        _metrosAvanzados = _intialMetrosAvanzados;
    }

    private void Update()
    {
        _metrosAvanzados += WorldSpeed * Time.deltaTime;
    }

    public static void SetWorldSpeed(float speed)
    {
        _worldSpeed = speed;
    }

    public static float MetrosAvanzados => _metrosAvanzados;
    public static float WorldSpeed => _worldSpeed;
}
