using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CombatComponent), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 12f;
    public LayerMask groundMask;

    private Rigidbody2D _rb;
    private CombatComponent _combatControls;
    private SpriteRenderer _spriteRenderer;

    // Public properties
    public bool IsMoving { get; private set; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _combatControls = GetComponent<CombatComponent>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        // Check if jump buttons are pressed
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            // Reset jump
            if (_rb.velocity.y == 0)
                // Check if feet have made contact
                if (Physics2D.Raycast(_rb.transform.position, -_rb.transform.up * Mathf.Sign(_rb.gravityScale), 2.0f, groundMask))
                {
                    // Check gravity scale direction
                    var jumpDirection = 1.0f;
                    if (_rb.gravityScale < 0.0f)
                        jumpDirection = -1.0f;

                    _rb.AddForce(Vector2.up * (jumpForce * jumpDirection), ForceMode2D.Impulse);
                    _rb.gravityScale = Mathf.Sign(_rb.gravityScale) * 2.0f;
                }

        // Movement
        var horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * (horizontal * speed * Time.deltaTime));

        // Update movement status flag
        IsMoving = horizontal is > 0.01f or < -0.01f;

        switch (horizontal)
        {
            // Update location of projectile spawn
            // based on movement direction
            case > 0.1f:
                _combatControls.TargetLocationFlipped(false);
                _spriteRenderer.flipX = false;
                break;
            case < -0.1f:
                _combatControls.TargetLocationFlipped(true);
                _spriteRenderer.flipX = true;
                break;
        }
    }
}