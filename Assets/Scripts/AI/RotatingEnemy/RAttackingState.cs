using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAttackingState : BaseState<REnemyStateMachine.REnemyStates>
{
    // Reference to FSM
    REnemyStateMachine FSM;

    // Constructor with call to base state class
    public RAttackingState() : base(REnemyStateMachine.REnemyStates.Attacking)
    { }

    // Handler for state enter
    public override void EnterState()
    {
        FSM = (REnemyStateMachine)OwningFSM;
    }

    // Handler for state exit
    public override void ExitState()
    {
        // Any clean up needed from this state will go here
    }

    // Handler for state update ticks
    public override void UpdateState()
    {
        // Turn to face target
        FSM.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, (FSM.target.position - FSM.gameObject.transform.position));

        // Get combat controls
        CombatComponent combatControls = FSM.CombatControls;

        // Get projectile target based on shooting direction
        Vector3 targetLoc = new Vector3();
        switch (combatControls.shootDirection)
        {
            case ShootDirection.Horizontal:
                targetLoc = new Vector3(FSM.target.position.x, FSM.gameObject.transform.position.y, 0.0f);
                break;
            case ShootDirection.Vertical:
                targetLoc = new Vector3(FSM.gameObject.transform.position.x, FSM.target.position.y, 0.0f);
                break;
            case ShootDirection.Any:
                targetLoc = new Vector3(FSM.target.position.x, FSM.target.position.y, 0.0f);
                break;
        }


        // Try launching a projectile
        combatControls.LaunchProjectile(targetLoc);
    }

    // Handler for state transitions
    public override REnemyStateMachine.REnemyStates GetNextState()
    {
        // Check distance between transform and if not close enough 
        // transfer into idle state
        if (FSM.PerceptionControls.detectedColliders.Count == 0)
            return REnemyStateMachine.REnemyStates.Idle;

        return REnemyStateMachine.REnemyStates.Attacking;
    }
}
