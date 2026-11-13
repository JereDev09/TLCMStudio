using UnityEngine;

public class ScrollObjects : MonoBehaviour
{
    [SerializeField] private float _horizontalSize;
    [SerializeField] private float _scrollFactorMultiplier;

    [Tooltip("si esta activo el terreno se repite si no, solo saldra de la pantalla y se quedara ahi (util para elementos unicos)")]
    [SerializeField] private bool _canRepeat = true;

    private void Update()
    {
        //para que si un objeto sale del area se deje de mover si no puede repetirse

        if (transform.position.x < -_horizontalSize) return;

        transform.position -= new Vector3(GameManager.WorldSpeed * _scrollFactorMultiplier,0,0) * Time.deltaTime;

        if (!_canRepeat) return;

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
