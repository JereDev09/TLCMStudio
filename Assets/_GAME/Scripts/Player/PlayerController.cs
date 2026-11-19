using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerData;

    [Tooltip("cuanta parte de la velocidad brindada se le pasara")]
    [Range(0f, 1f)]
    [SerializeField] private float _intialVelocityFactor;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private string _groundTag;

    private float lastValidYVelocity;

    private void OnEnable()
    {
        GameManager.OnGameEnd += GameEnd;
    }

    private void GameEnd()
    {
        
    }

    private void OnDisable()
    {
        GameManager.OnGameEnd -= GameEnd;
    }

    public void SetIntialVelocity(float velocity, Vector2 dir)
    {
        rb.linearVelocity = new Vector2(0f, dir.y * velocity * _intialVelocityFactor);
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocityY != 0f)
        {
            lastValidYVelocity = rb.linearVelocityY;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.GameEnded) return;

        if (collision.gameObject.CompareTag(_groundTag))
        {
            Debug.Log("chocoConGround");

            AplicarHit(playerData.playerOnHitData.hitOnGround);
        }
    }

    public void AplicarHit(HitData hitData)
    {
        if (hitData.bounceData.soloCayendo && lastValidYVelocity > 0f) return;

        GameManager.AddWorldSpeed(hitData.velocidad);

        if (GameManager.WorldSpeed <= 0f) return;

        SetNewVelocity(hitData.bounceData.GetNewYSpeed(lastValidYVelocity));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.GameEnded) return;

        IImpactable Entity = collision.GetComponent<IImpactable>();

        if (Entity != null)
        {
            Entity.OnImpact(this);
        }
    }

    public void SetNewVelocity(float velocity)
    {
        Debug.Log(velocity);

        rb.linearVelocity = new Vector2(0, velocity);
    }

    public float velocityY => rb.linearVelocityY;
    public PlayerHitData playerHitData => playerData.playerOnHitData;
}
