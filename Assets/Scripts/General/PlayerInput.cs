using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Declare component references
    CombatComponent _CombatControls;
    PlayerAnimationHandler _AnimControls;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize any component references or fields here
        _CombatControls = GetComponent<CombatComponent>();
        _AnimControls = GetComponent<PlayerAnimationHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        // Shoot functionality
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    _AnimControls.PlayAttack();
        //}
    }

    // Method that is called once it is time for projectile to launch
    public void EventLaunchProjectile()
    {
        _CombatControls.LaunchProjectile(gameObject.transform.position + (_CombatControls.shootLocation.position - gameObject.transform.position));
    }
}
