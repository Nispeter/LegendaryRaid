using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeedMultiplier = 1.5f;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private bool isRunning;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        InputManager.OnMove += HandleMove;
        InputManager.OnRun += HandleRun;
    }

    void OnDestroy()
    {
        InputManager.OnMove -= HandleMove;
        InputManager.OnRun -= HandleRun;
    }

    void HandleMove(Vector2 input)
    {
        movementInput = input;
    }

    void HandleRun(Vector2 input)
    {
        isRunning = input != Vector2.zero;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 moveDirection = movementInput.normalized;
        float currentSpeed = moveSpeed * (isRunning ? runSpeedMultiplier : 1f);
        Vector2 velocity = moveDirection * currentSpeed;

        rb.velocity = velocity;
    }
}
