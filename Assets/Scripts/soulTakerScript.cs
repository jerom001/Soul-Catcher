using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soulTakerScript : MonoBehaviour
{
    public GameObject soulTakerPrefab;
    public float timer;
    private float soulTakerInterval;
    public float minX;
    public float maxX;
    public float soulTakerHeight;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        StartCoroutine(WaitForGameActive());
    }

    IEnumerator WaitForGameActive()
    {
        yield return new WaitUntil(() => gameManager.isGameActive);
        yield return new WaitForSeconds(1f); // optional buffer to prevent early spawn
        enabled = true;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > soulTakerInterval)
        {
            soulTakerSpawn();
            timer = 0;
            soulTakerInterval = Random.Range(5f, 10f);
        }
    }

    void soulTakerSpawn()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 soulTakerPosition = new Vector3(randomX, soulTakerHeight, 0);
        Instantiate(soulTakerPrefab, soulTakerPosition, Quaternion.identity);
    }
}