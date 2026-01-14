using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSpawnScript : MonoBehaviour
{
    public GameObject SoulPrefab;
    public float minX;
    public float maxX;
    public float timer = 0f;
    public float soulInterval = 2;
    public float spawnHeight;
    public float minSoulInterval = 0.5f;
    public float difficultyIncreaseRate = 0.02f;
    public GameObject rareSoulPrefab;
    public GameObject soulTaker;
    [Range(0f, 1f)] public float rareSoulChance = 0.1f; // 10% chance

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        enabled = false; // Disable Update initially 
        StartCoroutine(WaitForGameActive());
    }

    IEnumerator WaitForGameActive()
    {
        yield return new WaitUntil(() => gameManager.isGameActive);
        yield return new WaitForSeconds(0.1f); // optional delay to let player get ready
        enabled = true; // now Update can run
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= soulInterval)
        {
            soulSpawn();
            timer = 0f;

        }
       
        if (soulInterval > minSoulInterval) 
        {
            soulInterval -= difficultyIncreaseRate;
        }

    }

    void soulSpawn()
    {
        float minDistance = 2f;
        float soulX = Random.Range(minX, maxX);
        float soulTakerX;

        do
        {
            soulTakerX = Random.Range(minX, maxX);
        } while (Mathf.Abs(soulTakerX - soulX) < minDistance);

        float randomx = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3 (randomx, spawnHeight, 0);

        GameObject prefabToSpawn = Random.value < rareSoulChance ? rareSoulPrefab : SoulPrefab;
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}
