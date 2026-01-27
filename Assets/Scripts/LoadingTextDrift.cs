using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingTextDrift : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    private float timer;
    private int dotCount = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 0.5f)
        {
            dotCount = (dotCount + 1) % 4;
            loadingText.text = "Loading" + new string('.', dotCount);
            timer = 0f;
        }
    }
}
