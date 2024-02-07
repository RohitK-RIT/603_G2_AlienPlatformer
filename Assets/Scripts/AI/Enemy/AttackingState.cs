using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : BaseState<EnemyStateMachine.EnemyStates>
{
    // Reference to FSM
    EnemyStateMachine FSM;

    // Constructor with call to base state class
    public AttackingState() : base(EnemyStateMachine.EnemyStates.Attacking)
    { }

    // Handler for state enter
    public override void EnterState()
    {
        FSM = (EnemyStateMachine)OwningFSM;
    }

    // Handler for state exit
    public override void ExitState()
    {
        // Any clean up needed from this state will go here
    }

    // Handler for state update ticks
    public override void UpdateState()
    {
        // Get combat controls
        CombatComponent combatControls = FSM.CombatControls;

        // Set enemy to face target
        SpriteRenderer spriteRenderer = FSM.SpriteRenderer;
        if (FSM.target.position.x < FSM.gameObject.transform.position.x)
        {
            spriteRenderer.flipX = true;
            combatControls.TargetLocationFlipped(true);
        }
        else
        {
            spriteRenderer.flipX = false;
            combatControls.TargetLocationFlipped(false);
        }

        // Try launching a projectile
        combatControls.LaunchProjectile(new Vector3(FSM.target.position.x, FSM.gameObject.transform.position.y, 0.0f));
    }

    // Handler for state transitions
    public override EnemyStateMachine.EnemyStates GetNextState()
    {
        // Check distance between transform and if not close enough 
        // transfer into idle state
        if (FSM.PerceptionControls.detectedColliders.Count == 0)
            return EnemyStateMachine.EnemyStates.Moving;

        return EnemyStateMachine.EnemyStates.Attacking;
    }
}
