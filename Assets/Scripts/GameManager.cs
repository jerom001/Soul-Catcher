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
    public GameObject blueSoulBurstPrefab;

    private bool gameOverStarted = false;
    private Coroutine shakeRoutine;

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
        if (Camera.main != null)
        {
            cameraShaker = Camera.main.GetComponent<cameraShaker>();
        }
        else
        {
            cameraShaker = null;
        }
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

        yield return new WaitForFixedUpdate();

        isGameActive = true;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMusic(AudioManager.Instance.gamePlayMusic);
    }

    public void IncreaseScore(int amount, Vector3 spawnPosition)
    {
        if (!isGameActive) return;

        score += amount;
        updateUI();
        if (blueSoulBurstPrefab != null)
        {
            GameObject effect = Instantiate(blueSoulBurstPrefab, spawnPosition, Quaternion.identity);
            Destroy(effect, 1f);
        }
        if (scorePopRoutine != null)
            StopCoroutine(scorePopRoutine);

        scorePopRoutine = StartCoroutine(ScorePop());
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX(AudioManager.Instance.pointSFX);
    }

    IEnumerator ScorePop()
    {
        float duration = 0.1f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
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

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX(AudioManager.Instance.damageSFX);

            StartShake(0.1f, 0.06f);

            if (lives == 0)
                GameOver();
        }
    }
    void StartShake(float duration, float magnitude)
    {
        if (cameraShaker == null) return;

        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(cameraShaker.Shake(duration, magnitude));
    }

    public void AddLife(Vector3 spawnPosition)
    {
        if (!isGameActive) return;

        if (lives < skullIcons.Length)
        {
            skullIcons[lives].SetActive(true);
            lives++;

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX(AudioManager.Instance.healSFX);
        }
    }

    void updateUI()
    {
        scoreUi.text = "Score: " + score;
    }

    void GameOver()
    {
        if (!isGameActive) return;
        if (gameOverStarted) return;

        gameOverStarted = true;
        isGameActive = false;

        if (scorePopRoutine != null)
            StopCoroutine(scorePopRoutine);

        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        // Slow motion impact
        Time.timeScale = 0.3f;

        StartShake(0.2f, 0.1f);
        yield return new WaitForSecondsRealtime(0.25f);

        Time.timeScale = 0f;

        darkOverlay.SetActive(true);
        finalScoreText.text = "Final Score: " + score;
        gameOverScreen.SetActive(true);
        scoreUi.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

 }
