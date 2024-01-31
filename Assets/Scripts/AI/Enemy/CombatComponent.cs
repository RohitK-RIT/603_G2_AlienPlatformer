using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatComponent : MonoBehaviour
{
    // Class references to be passed in
    public GameObject projectileClass;

    // Public fields
    public Transform shootLocation;
    public float projectileSpeed = 2.0f;

    // Fields for handling cooldown timer
    public float projectileCooldown = 1.0f;
    float projectileCooldownTimer = 0.0f;
    bool canShootProjectile = false;

    // Update is called once per frame
    void Update()
    {
        // Update timer if unable to shoot projectile
        if (!canShootProjectile)
        {
            projectileCooldownTimer -= Time.deltaTime;

            // Reset timer and shoot status
            if(projectileCooldownTimer <= 0.0f)
            {
                projectileCooldownTimer = projectileCooldown;
                canShootProjectile = true;
            }
        }
    }

    // Method that launches a projectile
    public void LaunchProjectile(Vector3 targetPos)
    {
        // Return if already false
        if (!canShootProjectile)
            return;

        // Update shoot status
        canShootProjectile = false;

        // Instantiate the projectile and set its velocity
        GameObject projectile = Instantiate(projectileClass, shootLocation.position, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = (targetPos - transform.position) * projectileSpeed;
    }
}
