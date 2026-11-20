using System;
using UnityEngine;

[Serializable]
public class PlayerHitData
{
    public HitData hitOnGround;
    public HitData hitOnTrampolin;
    public HitData hitOnBalcon;
    public HitData hitOnTecho;
    public HitData hitOnParedSolida;
}

[Serializable]
public class HitData
{
    [Tooltip("se suma a WorldSpeed; negativo frena")]
    public float velocidad;
    [Range(0f,1f)]
    public float perdidaPorcentual;
    public BounceStructure bounceData;
}

[Serializable]
public class BounceStructure
{
    [Tooltip("se usara cuando quieres que el objeto te de velocidad Y")]
    public TipoRebote tipoRebote;
    public bool soloCayendo;
    [Tooltip("cuanta velocidad en Y te dara a lo que ya tienes")]
    public float valorRebote;

    [Tooltip("cuanta velocidad en Y conservaras luego del rebote (pensado para objetos que los golpearas siempre cayendo)")]
    public float conservacionVelocidadY;

    public float GetNewYSpeed(float verticalSpeed)
    {
        switch (tipoRebote)
        {
            case TipoRebote.None:
                return verticalSpeed;
            case TipoRebote.Fijo:
                return Mathf.Max(valorRebote,verticalSpeed);
            case TipoRebote.Conservativo:
                return -(verticalSpeed * conservacionVelocidadY);
            default:
                return verticalSpeed;
        }
    }

}

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Scriptable Objects/PlayerDataSO")]
public class PlayerDataSO : ScriptableObject
{
    [Header("configuracion de cambio de velocidad")]
    public PlayerHitData playerOnHitData;

    //[Header("configuracion de rebote")]
    //public PlayerBounceData playerBounceData;

}
