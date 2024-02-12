using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REnemyStateMachine : StateMachine<REnemyStateMachine.REnemyStates>
{
    // Enum used for holding all creature state keys
    public enum REnemyStates
    {
        Idle,
        Attacking
    }

    // Private fields
    CombatComponent _CombatControls;
    SpriteRenderer _SpriteRenderer;
    PerceptionComponent _PerceptionControls;

    // Public fields
    public Transform target;
    public float startRotation;
    public float endRotation;

    // Public properties
    public CombatComponent CombatControls
    {
        get { return _CombatControls; }
    }
    public SpriteRenderer SpriteRenderer
    {
        get { return _SpriteRenderer; }
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
        states[REnemyStates.Idle] = new RIdleState();
        states[REnemyStates.Attacking] = new RAttackingState();

        // Default state will be wandering
        currentState = states[REnemyStates.Idle];
    }

    // Initialize the state machine's fields and components
    void Init()
    {
        _CombatControls = GetComponent<CombatComponent>();
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _PerceptionControls = GetComponent<PerceptionComponent>();
    }
}
