using System.Collections.Generic;
using UnityEngine;

namespace Features.Checkpoints
{
    public class CheckpointHandler : MonoBehaviour
    {
        public bool HasCheckpoint => _checkpoint.HasValue;

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
            if (checkpointPos.HasValue)
                _checkpoint = checkpointPos.Value + Vector2.up * 2f;
        }

        public void UseCheckpoint()
        {
            if (_checkpoint.HasValue)
                transform.position = new Vector3(_checkpoint.Value.x, _checkpoint.Value.y, transform.position.z);
            else
                Debug.LogError("Trying to get a checkpoint from a bad _checkpoints stack.");
        }
    }
}