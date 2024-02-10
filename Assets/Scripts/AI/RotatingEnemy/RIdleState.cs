using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RIdleState : BaseState<REnemyStateMachine.REnemyStates>
{
    // Reference to FSM
    REnemyStateMachine FSM;

    // Timer for SLERP rotation
    float slerpTimer = 0.0f;
    float slerpDirection = 1.0f;

    // Constructor with call to base state class
    public RIdleState() : base(REnemyStateMachine.REnemyStates.Idle)
    { }

    // Handler for state enter
    public override void EnterState()
    {
        FSM = (REnemyStateMachine)OwningFSM;
    }

    // Handler for state exit
    public override void ExitState()
    {

    }

    // Handler for state update ticks
    public override void UpdateState()
    {
        // Calculate Quaternions for either end of rotation cycle
        Quaternion qStart = Quaternion.AngleAxis(FSM.startRotation, Vector3.forward);
        Quaternion qEnd = Quaternion.AngleAxis(FSM.endRotation, Vector3.forward);

        // Update game object's rotation to scan for enemies
        FSM.gameObject.transform.rotation = Quaternion.Slerp(qStart, qEnd, slerpTimer);

        // Update timer
        slerpTimer += ((Time.deltaTime / 2.0f) * slerpDirection);

        // Change direction of slerp
        if (slerpTimer >= 1.0f)
            slerpDirection = -1.0f;
        else if(slerpTimer <= 0.0f)
            slerpDirection = 1.0f;

    }

    // Handler for state transitions
    public override REnemyStateMachine.REnemyStates GetNextState()
    {
        // Check for player detection and if spotted, switch
        // to attacking state
        if (FSM.PerceptionControls.detectedColliders.Count > 0)
            return REnemyStateMachine.REnemyStates.Attacking;

        return REnemyStateMachine.REnemyStates.Idle;
    }
}
