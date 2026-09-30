using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Gravity")]
    [SerializeField] private float fallGravityMultiplier = 2f;

    private Rigidbody2D rb;
    private float moveInput;
    private bool jumpRequested;
    private bool isGrounded;
    private float defaultGravityScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGravityScale = rb.gravityScale;
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        moveInput = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            moveInput = -1f;
        }

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            moveInput = 1f;
        }

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed || Keyboard.current.spaceKey.isPressed)
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
        Jump();
        ApplyGravity();
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        if (jumpRequested && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        jumpRequested = false;
    }

    private void ApplyGravity()
    {
        if (rb.linearVelocity.y < 0f)
        {
            rb.gravityScale = defaultGravityScale * fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = defaultGravityScale;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
