using UnityEngine;

namespace Features.Gravity_Modification
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class GravitySwitch : MonoBehaviour
    {
        private Rigidbody2D _rb;
        SpriteRenderer _SpriteRenderer;
        public GameObject gravityParticles;
        ParticleSystem gravityPS;

        // Start is called before the first frame update
        private void Start()
        {
            // Initialize components
            _rb = GetComponent<Rigidbody2D>();
            _SpriteRenderer = GetComponent<SpriteRenderer>();

            // Get max height and set particles to bottom
            float maxHeight = Camera.main.GetComponent<Camera>().orthographicSize;
            gravityParticles.transform.position = new Vector3(Camera.main.transform.position.x, -maxHeight, 0.0f);

            // Get particles from game object
            gravityPS = gravityParticles.GetComponentInChildren<ParticleSystem>();

        }

        private void Update()
        {
            if(Input.anyKey && Input.GetKeyDown(KeyCode.G) && _rb.velocity.y == 0.0f)
                FlipObjectGravity();
        }

        private void FlipObjectGravity()
        {
            _rb.gravityScale = -_rb.gravityScale;

            // Get max height and properly set particle position/rotation
            float maxHeight = Camera.main.GetComponent<Camera>().orthographicSize;
            if(_rb.gravityScale <= 0.0f)
            {
                gravityParticles.transform.position = Camera.main.transform.position + new Vector3(0.0f, -maxHeight, 1.0f);
                _SpriteRenderer.flipY = true;
            }
            else
            {
                gravityParticles.transform.position = Camera.main.transform.position + new Vector3(0.0f, maxHeight, 1.0f);
                _SpriteRenderer.flipY = false;
            }
            gravityParticles.transform.rotation *= Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 0.0f, 1.0f));

            // Play particles
            gravityPS.Play();
        }
    }
}
