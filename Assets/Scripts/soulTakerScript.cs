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

    void Start()
    {
        soulTakerInterval = Random.Range(5f, 10f);

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
