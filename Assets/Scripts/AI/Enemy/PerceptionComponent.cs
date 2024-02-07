using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionComponent : MonoBehaviour
{
    // Fields for player detection
    public float radius;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    // Limit angle to be between 0 and 360 degrees
    [Range(0, 360)]
    public float angle;

    // Used for storing detected colliders
    public List<Collider2D> detectedColliders;

    // Get sprite renderer
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Init detected objects list
        detectedColliders = new List<Collider2D>();

        // Init needed components
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Start player detection
        StartCoroutine(FOVRoutine());
    }

    // Delayed action used to detect player movement every 0.2 seconds
    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    // Method for detecting the player
    private void FieldOfViewCheck()
    {
        // Collider Array which will hold all possible targets
        Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);

        // Temp list used for getting the detected objects
        List<Collider2D> detectedTests = new List<Collider2D>();

        // If the array isnt empty that means the player is within the range of the agent
        if (rangeChecks.Length != 0)
        {
            // Iterate through each collider and check if in FOV
            foreach (Collider2D collider in rangeChecks)
            {
                // Get the target transform and distance between the two
                Transform target = collider.transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                // Calculate direction of FOV
                Vector3 FOVDirection;
                if (spriteRenderer.flipX)
                    FOVDirection = -transform.right;
                else
                    FOVDirection = transform.right;

                // Check if the player is within the FOV of the agent
                if (Vector3.Angle(FOVDirection, directionToTarget) < angle / 2)
                {
                    // Get distance between target and agent
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    // Raycast that checks for obstacles
                    if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                    {
                        // Add collider to list of detected tests
                        detectedTests.Add(collider);
                    }
                }
            }
        }

        // Set the detected colliders to the list of detected tests
        detectedColliders = detectedTests;
    }
}
