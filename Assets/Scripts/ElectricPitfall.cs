using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPitfall : MonoBehaviour
{
    // Public fields
    public List<Vector3> electrocutePoints;
    public float sparkCooldown = 3.0f;

    // Private fields
    ParticleSystem electricityPS;
    ParticleSystem sparksPS;
    float sparkTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Init components
        ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
        electricityPS = particleSystems[0];
        sparksPS = particleSystems[2];
    }

    // Update is called once per frame
    void Update()
    {
        // Handle spark timer and play spark effects every iteration
        sparkTimer -= Time.deltaTime;
        if(sparkTimer <= 0.0f)
        {
            PlaySparkEffect(electrocutePoints[Random.Range(0, electrocutePoints.Count)]);
            sparkTimer = sparkCooldown;
        }
    }

    // Method for playing the chain effect
    public void PlayShockEffect(Vector3 start, Vector3 end)
    {
        // Play effect
        electricityPS.Play();

        // Init emission params to override positional values
        var emitParams = new ParticleSystem.EmitParams();

        // Start pos
        emitParams.position = start;
        electricityPS.Emit(emitParams, 1);

        // Middle pos
        emitParams.position = (start + end) / 2.0f;
        electricityPS.Emit(emitParams, 1);

        // End pos
        emitParams.position = end;
        electricityPS.Emit(emitParams, 1);
    }

    // Method for playing spark effect
    public void PlaySparkEffect(Vector3 position)
    {
        // Play effect
        sparksPS.Play();

        // Init emission params to override positional values
        var emitParams = new ParticleSystem.EmitParams();

        // Start pos
        emitParams.position = position;
        sparksPS.Emit(emitParams, 1);
    }

    // Method used for detecting when player collides with pitfall
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if colliding with player
        if(collision.gameObject.CompareTag("Player"))
        {
            // Get animation controls and tell it to electrocute player
            PlayerAnimationHandler animControls = collision.gameObject.GetComponent<PlayerAnimationHandler>();

            // Play electricity effects
            PlayShockEffect(collision.gameObject.transform.position, electrocutePoints[Random.Range(0, electrocutePoints.Count)]);
            animControls.PlayElectrocute();
        }
    }
}
