using System;
using System.Collections;
using System.Collections.Generic;
using Features.Checkpoints;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Features.Gravity_Modification;

// Public enum for shooting directions
public enum ShootDirection
{
    Horizontal,
    Vertical,
    Any
}

// Public enum for shooting mode
public enum ShootMode
{
    SemiAuto,
    Burst
}

public class CombatComponent : MonoBehaviour
{
    public RectTransform healthBar;

    // Class references to be passed in
    public GameObject projectileClass;

    // Public fields
    public Transform shootLocation;
    public float projectileSpeed = 2.0f;
    public float health = 100.0f;
    public ShootDirection shootDirection;
    public ShootMode shootMode = ShootMode.SemiAuto;
    public float NormalizedHealth => health / 100f;

    // Fields for handling cooldown timer
    public float projectileCooldown = 1.0f;
    public int burstProjectileCount = 3;
    float projectileCooldownTimer = 0.0f;
    bool canShootProjectile = false;

    private CheckpointHandler _checkpointHandler;
    PlayerAnimationHandler _PlayerAnimControls;
    Rigidbody2D _rb;

    private void Start()
    {
        _checkpointHandler = GetComponent<CheckpointHandler>();
        _PlayerAnimControls = GetComponent<PlayerAnimationHandler>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update timer if unable to shoot projectile
        if (!canShootProjectile)
        {
            projectileCooldownTimer -= Time.deltaTime;

            // Reset timer and shoot status
            if (projectileCooldownTimer <= 0.0f)
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

        // Launch projectiles based on shoot mode
        switch (shootMode)
        {
            case ShootMode.SemiAuto:
                // Launch 1 projectile
                CreateProjectile(targetPos);
                break;
            case ShootMode.Burst:
                // Launch multiple projectiles
                StartCoroutine(BurstRoutine(targetPos, burstProjectileCount));
                break;
        }
    }

    // Method for instantiating projectile
    void CreateProjectile(Vector3 targetPos)
    {
        // Instantiate the projectile and set its velocity
        GameObject projectile = Instantiate(projectileClass, shootLocation.position, transform.rotation);
        projectile.GetComponentInChildren<Rigidbody2D>().velocity = (targetPos - transform.position) * projectileSpeed;
    }

    // Delayed action used to create a projectile every 0.2 seconds
    private IEnumerator BurstRoutine(Vector3 targetPos, int numProjectiles)
    {
        // Set delay
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        // Track number of projectiles
        int projectilesShot = 0;

        // Keep shooting every delay until desired projectiles are shot
        while (projectilesShot < numProjectiles)
        {
            yield return wait;
            CreateProjectile(targetPos);
            projectilesShot++;
        }
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
        if (healthBar)
            healthBar.localScale = new Vector2(NormalizedHealth, 1f);

        // Activate damage indicator
        ShowDamage();

        // Death handling
        if (health <= 0.0f)
        {
            // For the player
            if (gameObject.CompareTag("Player"))
            {
                _PlayerAnimControls.PlayDeath();
            }
            // For enemies
            else if (gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }
    }

    // Method used for handling damage indication
    public void ShowDamage()
    {
        // Get the sprite renderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Change sprite color to red
        if (spriteRenderer != null)
            spriteRenderer.color = Color.red;

        // Set color to be reset after a split second
        Invoke("ResetColor", 0.1f);
    }

    // Method used for resetting color
    public void ResetColor()
    {
        // Get the sprite renderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Change sprite color to red
        if (spriteRenderer != null)
            spriteRenderer.color = Color.white;
    }

    // Method for handling player death
    public void HandlePlayerDeath()
    {
        if (_checkpointHandler.TryUseCheckpoint())
        {
            // Reset health
            health = 100f;

            // Reset health bar
            if (healthBar)
                healthBar.localScale = new Vector2(NormalizedHealth, 1f);

            // Reset gravity
            if (_rb.gravityScale < 0.0f)
                GetComponent<GravitySwitch>().FlipObjectGravity();
        }
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}