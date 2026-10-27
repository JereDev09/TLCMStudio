using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed= 5f;
    public float jumpForce = 5f;

    [Header("Ground Check")]
    public bool isGrounded;
    public float MoveInput;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        InputManagerControiller.OnMove += Move;
        InputManagerControiller.OnJump += Jump;
    }
    private void OnDisable()
    {
        InputManagerControiller.OnMove -= Move;
        InputManagerControiller.OnJump -= Jump;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(MoveInput * speed, rb.linearVelocity.y);
    }
    private void Move(Vector2 input)
    {
        MoveInput = input.x;
    }

    private void Jump()
    {
        if (!isGrounded) return;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x,0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

