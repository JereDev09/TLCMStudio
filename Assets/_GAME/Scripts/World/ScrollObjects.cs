using UnityEngine;

public class ScrollObjects : MonoBehaviour
{
    [SerializeField] protected float _horizontalSize;
    [SerializeField] protected float _scrollFactorMultiplier;

    [Tooltip("si esta activo el terreno se repite si no, solo saldra de la pantalla y se quedara ahi (util para elementos unicos)")]
    [SerializeField] protected bool _canRepeat = true;

    private bool _finalizado;

    protected virtual void Awake() { }
    protected virtual float LimiteSalida => -_horizontalSize;


    private void Update()
    {
        if (_finalizado) return;

        transform.position -= new Vector3(GameManager.WorldSpeed * _scrollFactorMultiplier,0,0) * Time.deltaTime;

        if (_canRepeat)
        {
            if (transform.position.x < -_horizontalSize)
            {
                transform.position += new Vector3(_horizontalSize, 0, 0);
            }
            else if (transform.position.x > 0)
            {
                transform.position -= new Vector3(_horizontalSize, 0, 0);
            }
        }
        else if (transform.position.x < LimiteSalida)
        {
            _finalizado = true;
            OnFinalAlcanzado();
        }
    }

    protected virtual void OnFinalAlcanzado() { }
}
