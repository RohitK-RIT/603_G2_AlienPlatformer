using System.Collections.Generic;
using UnityEngine;

namespace Features.Checkpoints
{
    public class CheckpointHandler : MonoBehaviour
    {
        private Vector2? _checkpoint;

        private void Start()
        {
            _checkpoint = null;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Checkpoint"))
                return;

            var checkpointPos = other.GetComponent<Checkpoint>().GetCheckpointPos();
            _checkpoint = checkpointPos + Vector2.up * 2f;
        }

        public bool TryUseCheckpoint()
        {
            if (!_checkpoint.HasValue)
                return false;

            transform.position = new Vector3(_checkpoint.Value.x, _checkpoint.Value.y, transform.position.z);
            return true;
        }
    }
}