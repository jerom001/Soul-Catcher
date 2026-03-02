using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSpawnScript : MonoBehaviour
{

    public float minX;
    public float maxX;
    public float spawnHeight;

    public float soulInterval = 2f;
    public float minSoulInterval = 0.5f;
    public float difficultyIncreaseRate = 0.02f;

    public GameObject normalSoulPrefab;
    public GameObject rareSoulPrefab;
    [Range(0f, 1f)] public float rareSoulChance = 0.1f;

    private float timer;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("[SoulSpawnScript] GameManager not found in scene.");
            enabled = false;
            return;
        }

        timer = 0f;
    }

    void Update()
    {
        if (gameManager == null) return;
        if (!gameManager.isGameActive) return;

        timer += Time.deltaTime;

        float difficultyLevel = gameManager.score;

        float dynamicInterval = soulInterval - (difficultyLevel * difficultyIncreaseRate);
        dynamicInterval = Mathf.Max(minSoulInterval, dynamicInterval);

        if (timer >= dynamicInterval)
        {
            SpawnSoul(difficultyLevel);
            timer = 0f;
        }
    }

    void SpawnSoul(float difficultyLevel)
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0);

        float adjustedRareChance = rareSoulChance - (difficultyLevel * 0.01f);
        adjustedRareChance = Mathf.Clamp(adjustedRareChance, 0.02f, rareSoulChance);

        GameObject prefab =
            Random.value < adjustedRareChance ? rareSoulPrefab : normalSoulPrefab;

        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
