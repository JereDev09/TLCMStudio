using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _worldSpeed;
    [SerializeField] private float _metrosAvanzados;
 
    private void Update()
    {
        _metrosAvanzados += WorldSpeed * Time.deltaTime;
    }

    public void SetWorldSpeed(float speed)
    {
        _worldSpeed = speed;
    }

    public float MetrosAvanzados => _metrosAvanzados;
    public float WorldSpeed => _worldSpeed;
}
