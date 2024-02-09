using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) 
            return;
        
        var otherCombatControls = other.GetComponent<CombatComponent>();
        if (otherCombatControls)
            otherCombatControls.TakeDamage(100);
    }
}