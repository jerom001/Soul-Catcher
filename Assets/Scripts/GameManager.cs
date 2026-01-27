using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;
    public int lives;
    public bool isGameActive = true;

    public Text scoreUi;
    public GameObject[] skullIcons;
    public GameObject gameOverScreen;
    public cameraShaker cameraShake;

    public CanvasGroup loadingScreen;
    public float loadingTime = 2f;

    void Start()
    {
        score = 0;
        lives = skullIcons.Length;
        updateUI();
        gameOverScreen.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine(FadeOutLoadingScreen());
    }

    public void IncreaseScore(int amount)
    {
        if (!isGameActive) return;

        score += amount;
        updateUI();
        Debug.Log("Score: " + score);
    }

    public void DecreaseLives()
    {
        if (!isGameActive || lives <= 0) return;  // ✅ prevent messing with lives after game over

        lives--;
        skullIcons[lives].SetActive(false);
        StartCoroutine(cameraShaker.instance.Shake(0.2f, 0.1f));
        updateUI();

        if (lives <= 0)
        {
            isGameActive = false; // ✅ set this right before GameOver
            GameOver();
        }
    }


    void updateUI()
    {
        scoreUi.text = "Score: " + score;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        isGameActive = false;  // ✅ double safety
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }


    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator FadeOutLoadingScreen()
    {
        float timer = 0f;

        while (timer < loadingTime)
        {
            timer += Time.deltaTime;
            loadingScreen.alpha = Mathf.Lerp(1, 0, timer / loadingTime);
            yield return null;
        }

        loadingScreen.gameObject.SetActive(false);
        isGameActive = true;
    }
}
