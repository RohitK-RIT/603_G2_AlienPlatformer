using System;
using System.Collections;
using UnityEngine;

namespace Features.Platforms
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class TrapPlatform : MonoBehaviour
    {
        enum Axis
        {
            X,
            Y
        }

        [SerializeField] private Vector2 trapDirection = Vector2.up;
        [SerializeField] private Axis trapAxis = Axis.Y;
        [SerializeField] private float timeToTrap = 2f;
        private Vector2 _normalizeTrapDirection;
        private Coroutine _trapCoroutine;

        private void Awake()
        {
            _normalizeTrapDirection = trapDirection.normalized;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_trapCoroutine != null)
                return;
            if (!other.gameObject.CompareTag("Player"))
                return;

            var playerDirection = (other.transform.position - transform.position).normalized;

            if (trapAxis == Axis.Y && _normalizeTrapDirection.y.Equals(Mathf.Round(playerDirection.y))
                || trapAxis == Axis.X && _normalizeTrapDirection.x.Equals(Mathf.Round(playerDirection.x)))
                _trapCoroutine = StartCoroutine(ActivateTrap());
        }

        private IEnumerator ActivateTrap()
        {
            yield return new WaitForSeconds(0.5f);
            
            var startPos = (Vector2)transform.position;
            var targetPos = startPos + trapDirection;
            var time = 0f;

            while (Vector2.Distance(targetPos, transform.position) >= 0.25f)
            {
                transform.position = Vector2.Lerp(startPos, targetPos, time / timeToTrap);
                time += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPos;

            time = 0;
            while (Vector2.Distance(transform.position, targetPos) >= 0.25f)
            {
                transform.position = Vector2.Lerp(targetPos, startPos, time / timeToTrap);
                time += Time.deltaTime;
                yield return null;
            }

            transform.position = startPos;
            _trapCoroutine = null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + trapDirection);
            Gizmos.color = Color.white;
        }
    }
}