using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<EState> where EState : System.Enum
{
    // Constructor
    public BaseState(EState key)
    {
        StateKey = key;
    }

    // Public properties
    public StateMachine<EState> OwningFSM { get; set; }
    public EState StateKey { get; private set; }

    // Methods needed for state implementation
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract EState GetNextState();

    // Overrideable methods optional for implementation
    public virtual void OnTriggerEnter(Collider other) { }
    public virtual void OnTriggerStay(Collider other) { }
    public virtual void OnTriggerExit(Collider other) { }
}
