using UnityEngine;

public class Chunk : ScrollObjects
{
    [SerializeField] private float _limiteSalida = -40f;

    public float BordeDerecho => transform.position.x + _horizontalSize;
    protected override float LimiteSalida => _limiteSalida - _horizontalSize;

    protected override void Awake()
    {
        _canRepeat = false;
        _scrollFactorMultiplier = 1f;
    }

    protected override void OnFinalAlcanzado()
    {
        Destroy(gameObject);
    }
}
