using UnityEngine;

namespace Features.Checkpoints
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Checkpoint : MonoBehaviour
    {
        private BoxCollider2D _boxCollider2D;

        private void Start()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _boxCollider2D.isTrigger = true;
            gameObject.tag = "Checkpoint";
        }

        public virtual Vector2 GetCheckpointPos()
        {
            _boxCollider2D.enabled = false;
            return transform.position;
        }
    }
}