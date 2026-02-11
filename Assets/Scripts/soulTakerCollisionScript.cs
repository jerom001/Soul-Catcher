using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soulTakerCollisionScript : MonoBehaviour
{
    private GameManager gameManager;
    private bool alreadyHit = false;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyHit) return;
        if (!gameManager.isGameActive) return;

        if (other.CompareTag("player"))
        {
            alreadyHit = true;
            gameManager.DecreaseLives();
            Destroy(gameObject);
        }
    }
}
