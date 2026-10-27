using UnityEngine;

public class ScrollObjects : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private float _horizontalSize;
    [SerializeField] private float _scrollFactorMultiplier;

    private void Update()
    {
        transform.position -= new Vector3(_gameManager.WorldSpeed * _scrollFactorMultiplier,0,0) * Time.deltaTime;

        if (transform.position.x < -_horizontalSize)
        {
            transform.position += new Vector3(_horizontalSize, 0, 0);
        }
        else if (transform.position.x > 0)
        {
            transform.position -= new Vector3(_horizontalSize, 0, 0);
        }
    }
}
