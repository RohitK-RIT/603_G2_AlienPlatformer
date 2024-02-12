using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderActivation : MonoBehaviour
{
    [SerializeField] GameObject objectToActivate;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            objectToActivate.SetActive(true);
        }
    }
}
