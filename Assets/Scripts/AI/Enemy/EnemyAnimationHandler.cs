using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    // Get the animator instance
    Animator animator;

    // Declare any animation hashes here
    int movingHash;

    // Get rigid body component
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Init animator reference and hashes
        animator = gameObject.GetComponentInChildren<Animator>();
        movingHash = Animator.StringToHash("IsMoving");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update moving hash with walk status
        if (rb.velocity.magnitude > 0.0f)
            animator.SetBool(movingHash, true);
        else
            animator.SetBool(movingHash, false);
    }
}
