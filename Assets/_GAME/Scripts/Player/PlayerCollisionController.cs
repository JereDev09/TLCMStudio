using System;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    public static event Action<Collision2D> ChocoConCollider;
    public static event Action<Collider2D> ChocoConTrigger;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChocoConCollider?.Invoke(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChocoConTrigger?.Invoke(collision);
    }
}
