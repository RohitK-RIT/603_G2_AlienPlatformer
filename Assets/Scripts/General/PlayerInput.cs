using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Declare component references
    CombatComponent _CombatControls;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize any component references or fields here
        _CombatControls = GetComponent<CombatComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Shoot functionality
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            _CombatControls.LaunchProjectile(gameObject.transform.position + (_CombatControls.shootLocation.position - gameObject.transform.position));
        }
    }
}
