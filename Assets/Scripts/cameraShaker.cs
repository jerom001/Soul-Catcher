using System.Collections;
using UnityEngine;

public class cameraShaker : MonoBehaviour
{
    public static cameraShaker instance;
    private bool isShaking = false;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        if (isShaking || Time.timeScale == 0) yield break; // prevent shake when paused or already shaking

        isShaking = true;
        Vector3 originalPos = Camera.main.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.unscaledDeltaTime; // use unscaled time in case timeScale is changed
            yield return null;
        }

        Camera.main.transform.localPosition = originalPos;
        isShaking = false;
    }
}
