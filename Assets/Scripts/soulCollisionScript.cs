using System.Collections;
using UnityEngine;

public class soulCollisionScript : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject floatingTextPrefab;
    private bool alreadyCollided = false;

    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyCollided) return;

        if (other.CompareTag("player"))
        {
            alreadyCollided = true;

            if (gameManager.isGameActive)
            {
                SoulType soulType = GetComponent<SoulType>();

                if (soulType != null && soulType.isHealing)
                {
                    if (gameManager.lives < gameManager.skullIcons.Length)
                    {
                        gameManager.AddLife();
                        ShowFloatingText("+1", Color.green);
                    }
                }
                else
                {
                    gameManager.IncreaseScore(1, transform.position);
                    playerMovement player = other.GetComponent<playerMovement>();
                    if (player != null)
                    {
                        player.TriggerGlow();
                    }
                    ShowFloatingText("1", Color.white);
                }
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag("Bottom"))
        {
            alreadyCollided = true;

            if (gameManager.isGameActive)
            {
                gameManager.DecreaseLives();
            }

            Destroy(gameObject);
        }
    }

    void ShowFloatingText(string text, Color color)
    {
        if (floatingTextPrefab != null)
        {
            GameObject ft = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            FloatingText floating = ft.GetComponent<FloatingText>();
            floating.SetText(text, color);
        }

    }
}
