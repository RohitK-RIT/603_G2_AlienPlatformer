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
        Attacking
    }

    // Private fields
    CombatComponent _CombatControls;
    SpriteRenderer _SpriteRenderer;
    AIMovementComponent _MovementControls;
    PerceptionComponent _PerceptionControls;

    // Public fields
    public Transform target;

    // Public properties
    public CombatComponent CombatControls
    {
        get { return _CombatControls; }
    }
    public SpriteRenderer SpriteRenderer
    {
        get { return _SpriteRenderer; }
    }
    public AIMovementComponent MovementControls
    {
        get { return _MovementControls; }
    }
    public PerceptionComponent PerceptionControls
    {
        get { return _PerceptionControls; }
    }


    void Awake()
    {
        // Initialize all components
        Init();

        // Fill the dictionary with needed states
        states[EnemyStates.Idle] = new IdleState();
        states[EnemyStates.Moving] = new MovingState();
        states[EnemyStates.Attacking] = new AttackingState();

        // Default state will be wandering
        currentState = states[EnemyStates.Moving];
    }

    // Initialize the state machine's fields and components
    void Init()
    {
        _CombatControls = GetComponent<CombatComponent>();
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _MovementControls = GetComponent<AIMovementComponent>();
        _PerceptionControls = GetComponent<PerceptionComponent>();
    }
}
