using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    // Get the animator instance
    Animator animator;

    // Declare any animation hashes here
    int movingHash;
    int jumpHash;
    int attackHash;

    // Get rigid body component
    Rigidbody2D rb;

    // Private fields
    PlayerMovement _MovementControls;

    // Start is called before the first frame update
    void Start()
    {
        // Init animator reference and hashes
        animator = gameObject.GetComponent<Animator>();
        movingHash = Animator.StringToHash("IsMoving");
        jumpHash = Animator.StringToHash("IsJumping");
        attackHash = Animator.StringToHash("Attack");
        rb = GetComponent<Rigidbody2D>();
        _MovementControls = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update moving hash with walk status
        animator.SetBool(movingHash, _MovementControls.IsMoving);

        // Update jump status based on y velocity
        animator.SetBool(jumpHash, rb.velocity.y > 0.01f || rb.velocity.y < -0.1f);
    }

    // Method for playing attack animation
    public void PlayAttack()
    {
        // Play if not already attacking
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("LaserAttack"))
            animator.SetTrigger(attackHash);
    }
}
