using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHeight : MonoBehaviour
{
    private GameObject player;
    [SerializeField] int deathHeight = -10;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        // Check height level
        if (player.transform.position.y < deathHeight)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
