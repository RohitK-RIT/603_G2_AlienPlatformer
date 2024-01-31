using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState<EnemyStateMachine.EnemyStates>
{
    // Reference to FSM
    EnemyStateMachine FSM;

    // Constructor with call to base state class
    public IdleState() : base(EnemyStateMachine.EnemyStates.Idle)
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

    }

    // Handler for state transitions
    public override EnemyStateMachine.EnemyStates GetNextState()
    {
        // Check distance between transform and if close enough 
        // transfer into attack state
        if (Vector3.Distance(FSM.target.position, FSM.transform.position) <= 4.0f)
            return EnemyStateMachine.EnemyStates.Attacking;

        return EnemyStateMachine.EnemyStates.Idle;
    }
}
