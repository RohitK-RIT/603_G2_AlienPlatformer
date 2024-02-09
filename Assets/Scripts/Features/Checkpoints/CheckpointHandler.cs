using System.Collections.Generic;
using UnityEngine;

namespace Features.Checkpoints
{
    public class CheckpointHandler : MonoBehaviour
    {
        public bool HasCheckpoint => _checkpoints.Count > 0;

        private Stack<Vector2> _checkpoints;

        private void Start()
        {
            _checkpoints = new Stack<Vector2>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Checkpoint"))
                return;

            var checkpointPos = other.GetComponent<Checkpoint>().GetCheckpointPos();
            if (checkpointPos.HasValue)
                _checkpoints.Push(checkpointPos.Value + Vector2.up * 2f);
        }

        public void UseCheckpoint()
        {
            if (_checkpoints.TryPop(out var lastCheckpoint))
                transform.position = new Vector3(lastCheckpoint.x, lastCheckpoint.y, transform.position.z);
            else
                Debug.LogError("Trying to get a checkpoint from a bad _checkpoints stack.");
        }
    }
}