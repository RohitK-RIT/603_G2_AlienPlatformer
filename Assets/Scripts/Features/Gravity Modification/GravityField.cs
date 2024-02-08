using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.Gravity_Modification
{
    [RequireComponent(typeof(EdgeCollider2D))]
    public class GravityField : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<EdgeCollider2D>().isTrigger = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (gameObject.CompareTag("Player"))
                Physics2D.gravity = Vector2.down * 9.8f * Random.Range(0.5f, 1.5f);
        }
    }
}