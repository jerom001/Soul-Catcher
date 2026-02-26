using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public int lives;
    public bool isGameActive = false;

    public Text scoreUi;
    public GameObject[] skullIcons;
    public GameObject gameOverScreen;
    public Text finalScoreText;
    public CanvasGroup loadingScreen;
    public float loadingTime = 2f;
    public GameObject darkOverlay;

    void Start()
    {
        darkOverlay.SetActive(false);
        score = 0;
        lives = skullIcons.Length;

        updateUI();
        gameOverScreen.SetActive(false);
        Time.timeScale = 1;

        isGameActive = false;
        StartCoroutine(StartGameSequence());
    }

    IEnumerator StartGameSequence()
    {
        float timer = 0f;

        while (timer < loadingTime)
        {
            timer += Time.deltaTime;
            loadingScreen.alpha = Mathf.Lerp(1, 0, timer / loadingTime);
            yield return null;
        }

        loadingScreen.gameObject.SetActive(false);

        // Wait one physics step to guarantee collisions work
        yield return new WaitForFixedUpdate();

        isGameActive = true;

        AudioManager.Instance.PlayMusic(AudioManager.Instance.gamePlayMusic);

    }

    public void IncreaseScore(int amount)
    {
        if (!isGameActive) return;

        score += amount;
        updateUI();

        AudioManager.Instance.PlaySFX(AudioManager.Instance.pointSFX);
    }

    public void DecreaseLives()
    {
        if (!isGameActive) return;

        if (lives > 0)
        {
            lives--;
            skullIcons[lives].SetActive(false);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.damageSFX);

            if (lives == 0)
                GameOver();
        }
    }

    public void AddLife()
    {
        if (!isGameActive) return;

        if (lives < skullIcons.Length)
        {
            skullIcons[lives].SetActive(true);
            lives++;

            AudioManager.Instance.PlaySFX (AudioManager.Instance.healSFX);
        }
    }

    void updateUI()
    {
        scoreUi.text = "Score: " + score;
    }

    void GameOver()
    {
        darkOverlay.SetActive(true);
        isGameActive = false;
        finalScoreText.text = "Final Score: " + score;
        gameOverScreen.SetActive(true);
        scoreUi.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
