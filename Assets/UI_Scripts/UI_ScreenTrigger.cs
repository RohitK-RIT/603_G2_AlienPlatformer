using System;
using UnityEngine;
using Random = UnityEngine.Random;

    [RequireComponent(typeof(EdgeCollider2D))]
    public class UI_ScreenTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject UIScreenToTrigger;      
        private void Start()
        {
            GetComponent<EdgeCollider2D>().isTrigger = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
                UIScreenToTrigger.SetActive(true);
        }
    }
