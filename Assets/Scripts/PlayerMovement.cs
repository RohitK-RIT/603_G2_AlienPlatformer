using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasJump = true;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 12f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        // Jump
        if (hasJump && (Input.GetAxis("Vertical") > 0 || Input.GetKeyDown(KeyCode.Space)))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
    }
}
