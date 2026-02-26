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
    private cameraShaker cameraShaker;
    private Coroutine scorePopRoutine;

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
        cameraShaker = Camera.main.GetComponent<cameraShaker>();

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

        if (scorePopRoutine != null)
            StopCoroutine(scorePopRoutine);

        scorePopRoutine = StartCoroutine(ScorePop());

        AudioManager.Instance.PlaySFX(AudioManager.Instance.pointSFX);
    }
    IEnumerator ScorePop()
    {
        float duration = 0.1f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float scale = Mathf.Lerp(1.2f, 1f, timer / duration);
            scoreUi.transform.localScale = Vector3.one * scale;
            yield return null;
        }

        scoreUi.transform.localScale = Vector3.one;
    }

    public void DecreaseLives()
    {
        if (!isGameActive) return;

        if (lives > 0)
        {
            lives--;
            skullIcons[lives].SetActive(false);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.damageSFX);
            StartCoroutine(cameraShaker.Shake(0.1f, 0.06f));

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
        if (!isGameActive) return;

        isGameActive = false;
        StartCoroutine(GameOverSequence());
    }
    IEnumerator GameOverSequence()
    {
        // Slow motion impact
        Time.timeScale = 0.3f;

        StartCoroutine(cameraShaker.Shake(0.2f, 0.1f));

        yield return new WaitForSecondsRealtime(0.25f);

        Time.timeScale = 0f;

        darkOverlay.SetActive(true);
        finalScoreText.text = "Final Score: " + score;
        gameOverScreen.SetActive(true);
        scoreUi.gameObject.SetActive(false);
    }
    IEnumerator SlowMoGameOver()
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
