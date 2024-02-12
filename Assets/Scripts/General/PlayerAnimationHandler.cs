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
    int electrocuteHash;
    int deathHash;
    int isAliveHash;

    // Get rigid body component
    Rigidbody2D rb;

    // Private fields
    PlayerMovement _MovementControls;
    CombatComponent _CombatControls;

    // Start is called before the first frame update
    void Start()
    {
        // Init animator reference and hashes
        animator = gameObject.GetComponent<Animator>();
        movingHash = Animator.StringToHash("IsMoving");
        jumpHash = Animator.StringToHash("IsJumping");
        electrocuteHash = Animator.StringToHash("Electrocute");
        deathHash = Animator.StringToHash("Death");
        isAliveHash = Animator.StringToHash("IsAlive");
        rb = GetComponent<Rigidbody2D>();
        _MovementControls = GetComponent<PlayerMovement>();
        _CombatControls = GetComponent<CombatComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update moving hash with walk status
        animator.SetBool(movingHash, _MovementControls.IsMoving);

        // Update jump status based on y velocity
        animator.SetBool(jumpHash, rb.velocity.y > 0.01f || rb.velocity.y < -0.1f);

        // Update health status
        animator.SetBool(isAliveHash, _CombatControls.health > 0.0f);
    }

    // Method for playing death animation
    public void PlayDeath()
    {
        // Play if not already attacking
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            animator.SetTrigger(deathHash);
    }

    // Method for playing electrocute animation
    public void PlayElectrocute()
    {
        // Play if not already attacking
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Electrocute"))
            animator.SetTrigger(electrocuteHash);
    }
}
