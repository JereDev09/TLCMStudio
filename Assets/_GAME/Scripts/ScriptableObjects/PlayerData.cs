using System;
using UnityEngine;

[Serializable]
public class PlayerHitData
{
    public float hitGround;
}

[Serializable]
public class PlayerBounceData
{
    public BounceStructure bounceOnGround;
}

[Serializable]
public class BounceStructure
{
    [Tooltip("se usara cuando quieres que el objeto te de velocidad Y")]
    public bool usarReboteFijo;
    [Tooltip("cuanta velocidad en Y te dara a lo que ya tienes")]
    public float valorRebote;

    [Tooltip("cuanta velocidad en Y conservaras luego del rebote (pensado para objetos que los golpearas siempre cayendo)")]
    public float conservacionVelocidadY;

    public float GetNewYSpeed(float verticalSpeed)
    {
        if (usarReboteFijo)
        {
            return verticalSpeed <= 0 ? valorRebote : verticalSpeed - valorRebote;
        }
        else
        {
            return -(verticalSpeed * conservacionVelocidadY);
        }
    }

}

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("configuracion de cambio de velocidad")]
    public PlayerHitData playerOnHitData;

    [Header("configuracion de rebote")]
    public PlayerBounceData playerBounceData;

}
