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
        // Turn to face the target
        Vector3 diff = FSM.target.position - FSM.gameObject.transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        FSM.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 180);

        // Try launching a projectile
        CombatComponent combatControls = FSM.CombatControls;
        combatControls.LaunchProjectile(FSM.target.position);
    }

    // Handler for state transitions
    public override EnemyStateMachine.EnemyStates GetNextState()
    {
        // Check distance between transform and if not close enough 
        // transfer into idle state
        if (Vector3.Distance(FSM.target.position, FSM.transform.position) >= 4.0f)
            return EnemyStateMachine.EnemyStates.Idle;

        return EnemyStateMachine.EnemyStates.Attacking;
    }
}
