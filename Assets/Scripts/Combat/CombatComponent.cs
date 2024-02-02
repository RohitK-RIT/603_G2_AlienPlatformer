using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatComponent : MonoBehaviour
{
    // Class references to be passed in
    public GameObject projectileClass;

    // Public fields
    public Transform shootLocation;
    public float projectileSpeed = 2.0f;
    public float health = 100.0f;

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
        projectile.GetComponentInChildren<Rigidbody2D>().velocity = (targetPos - transform.position) * projectileSpeed;
    }

    // Method used for flipping target location
    public void TargetLocationFlipped(bool isFlipped)
    {
        // Get horizontal distance between object and shoot loc
        Vector3 diff = gameObject.transform.position - new Vector3(shootLocation.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        float distance = diff.magnitude;

        // Offset X value if flipped
        if (isFlipped)
            shootLocation.position = gameObject.transform.position + new Vector3(-distance, 0.0f, 0.0f);
        else // Otherwise make sure its on the original side
            shootLocation.position = gameObject.transform.position + new Vector3(distance, 0.0f, 0.0f);
    }

    // Method called for handling damage of this object
    public void TakeDamage(float damage)
    {
        // Negate health
        health -= damage;

        // Death handling
        if(health <= 0.0f)
        {
            // For the player
            if(gameObject.tag == "Player")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            // For enemies
            if (gameObject.tag == "Enemy")
            {
                Destroy(gameObject);
            }
        }
    }
}
