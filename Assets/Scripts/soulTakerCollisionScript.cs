using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soulTakerCollisionScript : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            gameManager.DecreaseLives();
            Destroy(gameObject);

        }
    }
}
