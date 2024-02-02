using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Public fields
    public float lifeTime = 3.0f;
    public float damage = 25.0f;
    public GameObject impactParticles;

    // Update is called once per frame
    void Update()
    {
        // Update timer for life-time and destroy object once it ends
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0.0f)
            Destroy(gameObject);
    }

    // Method used for checking overlap collisions
    void OnTriggerEnter2D(Collider2D other)
    {
        // If the two game objects share the same tag
        // then ignore the collision and return
        if (gameObject.tag == other.gameObject.tag)
            return;

        // Try getting a combat component from other collider
        CombatComponent otherCombatControls = other.gameObject.GetComponent<CombatComponent>();
        if(otherCombatControls != null) // null check
            otherCombatControls.TakeDamage(damage);

        // Destroy this object
        Destroy(gameObject);

        // Spawn impact particles and destroy on timer
        GameObject impact = Instantiate(impactParticles, transform.position, transform.rotation);
        impact.GetComponent<ParticleSystem>().Play();
        Destroy(impact, 1.0f);
    }
}
