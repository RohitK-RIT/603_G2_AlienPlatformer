using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementComponent : MonoBehaviour
{
    // Public fields
    public List<Vector2> patrolPoints;
    public float maxSpeed = 2.0f;
    public float targetRadius = 0.5f;

    // Private fields
    Vector2 targetLocation;
    bool canMove = false;
    Vector2 currentVelocity;
    Rigidbody2D rb;
    SpriteRenderer _SpriteRenderer;

    // Patrol tracking fields
    int patrolIndex = 0;
    int patrolDirection = 1;

    // Public properties
    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        targetLocation = patrolPoints[patrolIndex];
    }

    // Update is called once per frame
    void Update()
    {
        // Move if capable
        if(canMove)
        {
            // Check if at destination and if so
            // update target to next patrol point
            if (IsAtDestination())
                UpdatePatrolPoint();
            UpdateVelocity();

            // Have sprite face toward movement direction
            if (rb.velocity.x < 0.0f)
                _SpriteRenderer.flipX = true;
            else
                _SpriteRenderer.flipX = false;
        }
    }

    // Method used for updating the current velocity 
    // to steer towards wander point
    public void UpdateVelocity()
    {
        // Desired velocity for target position
        Vector2 desiredVelocity = targetLocation - new Vector2(transform.position.x, transform.position.y);

        // Steer agent to the wander point
        currentVelocity += Steer(desiredVelocity.normalized * maxSpeed);

        // Update the rigid body velocity
        rb.velocity = new Vector3(currentVelocity.x, 0.0f, 0.0f);
    }

    // Method for returning a steering velocity
    // to reach a desired velocity
    public Vector2 Steer(Vector2 desired)
    {
        Vector2 steer = desired - currentVelocity;
        return steer;
    }

    // Method for checking if enemy has arrived at destination
    public bool IsAtDestination()
    {
        // Get the direction towards target position
        Vector2 direction = targetLocation - new Vector2(transform.position.x, transform.position.y);
        float distance = direction.magnitude;

        // Return result
        return distance <= targetRadius;
    }

    // Method that sets the current target to walk towards
    public void SetTarget(Vector3 newTargetLoc)
    {
        targetLocation = newTargetLoc;
    }

    // Method for updating patrol point
    public void UpdatePatrolPoint()
    {
        // Return early if not enough patrol points
        if (patrolPoints.Count < 2)
            return;

        // Change patrol direction if needed
        if (patrolIndex + 1 == patrolPoints.Count && patrolDirection == 1)
            patrolDirection = -1;
        else if (patrolIndex - 1 == -1 && patrolDirection == -1)
            patrolDirection = 1;

        // Update patrol index based on direction
        // and then set new target to that position
        patrolIndex += patrolDirection;
        SetTarget(patrolPoints[patrolIndex]);
    }
}
