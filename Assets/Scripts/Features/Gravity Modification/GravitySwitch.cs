using UnityEngine;

namespace Features.Gravity_Modification
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class GravitySwitch : MonoBehaviour
    {
        [SerializeField] private GameObject gravityParticles;
        
        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRenderer;
        private ParticleSystem _gravityPS;
        private Camera _mainCam;

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {
            // Initialize components
            _rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _mainCam = Camera.main;

            // Get max height and set particles to bottom
            gravityParticles.transform.position = new Vector3(_mainCam.transform.position.x, -_mainCam.orthographicSize, 0.0f);

            // Get particles from game object
            _gravityPS = gravityParticles.GetComponentInChildren<ParticleSystem>();
        }

        /// <summary>
        /// Update is called once every frame.
        /// </summary>
        private void Update()
        {
            if (Input.anyKey && Input.GetKeyDown(KeyCode.Space) && _rb.velocity.y == 0.0f)
                FlipObjectGravity();
        }

        /// <summary>
        /// Function called to flip the player's gravity.
        /// </summary>
        public void FlipObjectGravity()
        {
            // Reverse the gravity and scale it up
            var gravityScale = Mathf.Sign(_rb.gravityScale) * 4.0f;
            gravityScale = -gravityScale;
            _rb.gravityScale = gravityScale;

            // Get max height and properly set particle position/rotation
            gravityParticles.transform.position = _mainCam.transform.position
                                                  + new Vector3(0f, Mathf.Sign(gravityScale) * _mainCam.orthographicSize, 1f);
            _spriteRenderer.flipY = gravityScale <= 0f;
            gravityParticles.transform.rotation *= Quaternion.AngleAxis(180f, Vector3.forward);

            // Play particles
            _gravityPS.Play();
        }
    }
}