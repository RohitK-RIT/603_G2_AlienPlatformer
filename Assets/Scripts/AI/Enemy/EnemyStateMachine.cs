using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine<EnemyStateMachine.EnemyStates>
{
    // Enum used for holding all creature state keys
    public enum EnemyStates
    {
        Idle,
        Moving,
        Jumping,
        Attacking
    }

    // Private fields
    CombatComponent _CombatControls;

    // Public fields
    public Transform target;

    // Public properties
    public CombatComponent CombatControls
    {
        get { return _CombatControls; }
    }


    void Awake()
    {
        // Initialize all components
        Init();

        // Fill the dictionary with needed states
        states[EnemyStates.Idle] = new IdleState();
        states[EnemyStates.Attacking] = new AttackingState();

        // Default state will be wandering
        currentState = states[EnemyStates.Idle];
    }

    // Initialize the state machine's fields and components
    void Init()
    {
        _CombatControls = GetComponent<CombatComponent>();
    }
}
