using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class soulCollisionScript : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject floatingTextPrefab;

    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameManager.isGameActive) return;

        if (other.CompareTag("player"))
        {
            SoulType soulType = GetComponent<SoulType>();
            PlayParticles();

            if (soulType != null)
            {
                if (soulType.isHealing)
                {
                    if (gameManager.lives < gameManager.skullIcons.Length)
                    {
                        gameManager.lives++;
                        gameManager.skullIcons[gameManager.lives - 1].SetActive(true);
                        ShowFloatingText("+1 Life", Color.green);
                    }
                    else
                    {
                        Debug.Log("💚 Healing soul caught, but already at max lives.");
                    }
                }
                else if (soulType.isDamage)
                {
                    gameManager.DecreaseLives();
                    ShowFloatingText("-1 Life", Color.red);
                    StartCoroutine(cameraShaker.instance.Shake(0.1f, 0.05f));
                }
                else // Normal soul
                {
                    gameManager.IncreaseScore(1);
                    ShowFloatingText("+1", Color.white);
                }
            }
            else // SoulType is missing — assume it's a normal soul
            {
                gameManager.IncreaseScore(1);
                ShowFloatingText("+1", Color.white);
            }

            // ✅ Always play audio + destroy after interaction
            AudioSource audio = GetComponent<AudioSource>();
            if (audio != null && audio.clip != null)
            {
                audio.Play();
                Destroy(gameObject, 0.2f);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("Bottom"))
        {
            gameManager.DecreaseLives();
            StartCoroutine(cameraShaker.instance.Shake(0.1f, 0.05f));
            Debug.Log("💀 Soul missed. Life lost!");
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

    public GameObject collectParticles;

    void PlayParticles()
    {
        if (collectParticles != null)
        {
            GameObject fx = Instantiate(collectParticles, transform.position, Quaternion.identity);
            Destroy(fx, 1f); // destroy after a second
        }
    }

}
