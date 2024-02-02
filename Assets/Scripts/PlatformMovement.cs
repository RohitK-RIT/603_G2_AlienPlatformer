using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float distance = 1f;
    private bool isMovingRight = true;

    private float leftEnd;
    private float rightEnd;

    void Start()
    {
        // Set bounds of platform movement
        leftEnd = transform.position.x;
        rightEnd = transform.position.x + distance;
    }

    void Update()
    {
        // Move platform in direction
        if (isMovingRight)
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }

        // Boolean checks
        if (transform.position.x >= rightEnd) isMovingRight = false;
        if (transform.position.x <= leftEnd) isMovingRight = true;
    }

    // Move the player if it's on top of platform
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // Move player in platform direction
            if (isMovingRight)
            {
                collision.gameObject.transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            }
            else
            {
                collision.gameObject.transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
            }
            
        }
    }
}