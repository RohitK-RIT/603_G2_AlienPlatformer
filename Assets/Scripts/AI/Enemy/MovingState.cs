using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : BaseState<EnemyStateMachine.EnemyStates>
{
    // Reference to FSM
    EnemyStateMachine FSM;

    // Constructor with call to base state class
    public MovingState() : base(EnemyStateMachine.EnemyStates.Moving)
    { }

    // Handler for state enter
    public override void EnterState()
    {
        FSM = (EnemyStateMachine)OwningFSM;

        // Get movement component and enable movement
        FSM.MovementControls.CanMove = true;
    }

    // Handler for state exit
    public override void ExitState()
    {
        // Any clean up needed from this state will go here
        FSM.MovementControls.CanMove = false;
    }

    // Handler for state update ticks
    public override void UpdateState()
    {
        
    }

    // Handler for state transitions
    public override EnemyStateMachine.EnemyStates GetNextState()
    {
        // Check for player detection and if spotted, switch
        // to attacking state
        if (FSM.PerceptionControls.detectedColliders.Count > 0)
            return EnemyStateMachine.EnemyStates.Attacking;

        return EnemyStateMachine.EnemyStates.Moving;
    }
}
