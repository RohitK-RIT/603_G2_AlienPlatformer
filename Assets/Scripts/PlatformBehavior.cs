using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    [Header("Breakaway")]
    [SerializeField] bool isBreakaway = false;
    [SerializeField] float timeTillDestroy = 0.8f;

    [Header("Movement")]
    [SerializeField] bool isMoving = false;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float distance = 1f;
    private bool isMovingRight = true;
    private bool hasBroken = false;

    private float leftEnd;
    private float rightEnd;

    void Start()
    {
        // Set bounds of platform movement
        if (isMoving)
        {
            leftEnd = transform.position.x;
            rightEnd = transform.position.x + distance;
        }
    }

    void Update()
    {
        // Move platform in direction
        if (isMoving)
        {
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
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Move the player if it's on top of platform
        if (isMoving && collision.gameObject.tag == "Player")
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

        // Break away from platform if player is on top of it
        if (isBreakaway && collision.gameObject.tag == "Player")
        {
            if (hasBroken) return;
            StartCoroutine(DestroyPlatform());
        }
    }

    IEnumerator DestroyPlatform() 
    {
        hasBroken = true;
        yield return new WaitForSeconds(timeTillDestroy);

        // Destory Platform
        Destroy(gameObject);
    }
}
