using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasJump = true;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 12f;

    CombatComponent _CombatControls;
    SpriteRenderer _SpriteRenderer;
    bool _bIsMoving;

    // Public properties
    public bool IsMoving
    {
        get { return _bIsMoving; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _CombatControls = GetComponent<CombatComponent>();
        _SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // Jump
        if (hasJump && (Input.GetAxis("Vertical") > 0))
        {
            // Check gravity scale direction
            float jumpDirection = 1.0f;
            if (rb.gravityScale < 0.0f)
                jumpDirection = -1.0f;

            rb.AddForce(Vector2.up * jumpForce * jumpDirection, ForceMode2D.Impulse);
            hasJump = false;
        }

        // Reset jump
        if (rb.velocity.y == 0)
        {
            hasJump = true;
        }

        // Movement
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontal * speed * Time.deltaTime);

        // Update movement status flag
        if (horizontal > 0.01f || horizontal  < -0.01f)
            _bIsMoving = true;
        else
            _bIsMoving = false;

        // Update location of projectile spawn
        // based on movement direction
        if (horizontal > 0.1f)
        {
            _CombatControls.TargetLocationFlipped(false);
            _SpriteRenderer.flipX = false;
        }
        else if(horizontal < -0.1f)
        {
            _CombatControls.TargetLocationFlipped(true);
            _SpriteRenderer.flipX = true;
        }
    }
}
