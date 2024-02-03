using UnityEngine;

namespace Features.Gravity_Modification
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class GravitySwitch : MonoBehaviour
    {
        private Rigidbody2D _rb;

        // Start is called before the first frame update
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if(Input.anyKey && Input.GetKeyDown(KeyCode.G) && _rb.velocity.y == 0.0f)
                FlipObjectGravity();
        }

        private void FlipObjectGravity()
        {
            _rb.gravityScale = -_rb.gravityScale;
        }
    }
}
