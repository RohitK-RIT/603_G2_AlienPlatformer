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
        FSM.MovementControls.CanMove = false;
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
    public override EnemyStateMachine.EnemyStates GetNextState()
    {
        // Check distance between transform and if not close enough 
        // transfer into idle state
        if (FSM.PerceptionControls.detectedColliders.Count == 0)
            return EnemyStateMachine.EnemyStates.Moving;

        return EnemyStateMachine.EnemyStates.Attacking;
    }
}
