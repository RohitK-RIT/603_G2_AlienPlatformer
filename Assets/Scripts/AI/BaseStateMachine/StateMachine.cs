using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<EState> : MonoBehaviour where EState : System.Enum
{
    // Public fields
    public bool bIsActive = true;

    // Protected fields
    protected Dictionary<EState, BaseState<EState>> states = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> currentState;
    protected bool bIsTransitioningState = false;

    // Start is called at beginning of play
    void Start()
    {
        // Iterate through each state to pass game object as owner reference
        foreach (KeyValuePair<EState, BaseState<EState>> state in states)
        {
            state.Value.OwningFSM = this;
        }

        // Enter the first state
        currentState.EnterState();
    }

    // Update is called once every frame
    void Update()
    {
        // Early return if activity is disabled
        if (!bIsActive)
            return;

        // Get the next key from the current state
        // if a transition is ready
        EState nextStateKey = currentState.GetNextState();

        // Run current state's update when not transitioning
        if (!bIsTransitioningState && nextStateKey.Equals(currentState.StateKey))
            currentState.UpdateState();
        else // Transition case
            TransitionToState(nextStateKey);
    }

    // Transitions current state of state machine into a new state
    public void TransitionToState(EState stateKey)
    {
        // Exit the current state,
        // set the new state
        // enter that new state
        bIsTransitioningState = true;
        currentState.ExitState();
        currentState = states[stateKey];
        currentState.EnterState();
        bIsTransitioningState = false;
    }

    // Calls upon state trigger handler for enter
    void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    // Calls upon state trigger handler for stay
    void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStay(other);
    }

    // Calls upon state trigger handler for exit
    void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }
}
