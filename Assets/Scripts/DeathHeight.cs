using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHeight : MonoBehaviour
{
    [SerializeField] int deathHeight = -10;

    void Update()
    {
        // Check height level
        if (gameObject.transform.position.y < deathHeight)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
