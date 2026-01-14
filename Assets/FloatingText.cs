using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float lifetime = 1f;
    public float fadeSpeed = 2f;

    private TextMeshProUGUI text;
    private CanvasGroup canvasGroup;

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (!canvasGroup) canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        canvasGroup.alpha -= Time.deltaTime * fadeSpeed;

        if (canvasGroup.alpha <= 0)
            Destroy(gameObject);
    }

    public void SetText(string value, Color color)
    {
        if (text != null)
        {
            text.text = value;
            text.color = color;
        }
    }
}

