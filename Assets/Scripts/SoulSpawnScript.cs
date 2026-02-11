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
        timer = 0f;
    }

    void Update()
    {
        if (!gameManager.isGameActive) return;

        timer += Time.deltaTime;

        if (timer >= soulInterval)
        {
            SpawnSoul();
            timer = 0f;

            if (soulInterval > minSoulInterval)
                soulInterval -= difficultyIncreaseRate;
        }
    }

    void SpawnSoul()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0);

        GameObject prefab =
            Random.value < rareSoulChance ? rareSoulPrefab : normalSoulPrefab;

        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
