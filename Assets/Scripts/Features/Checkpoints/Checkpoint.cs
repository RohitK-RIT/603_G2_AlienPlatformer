using UnityEngine;

namespace Features.Checkpoints
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Checkpoint : MonoBehaviour
    {
        public bool Used { get; private set; }

        private void Start()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.tag = "Checkpoint";
        }

        public virtual Vector2? GetCheckpointPos()
        {
            if (Used)
                return null;
            Used = true;

            return transform.position;
        }
    }
}