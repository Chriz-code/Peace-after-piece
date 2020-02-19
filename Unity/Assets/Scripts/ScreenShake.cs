using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeDuration = 0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;
    void OnEnable()
    {
        originalPos = Camera.main.transform.localPosition;
    }
    void Update()
    {
        if (shakeDuration > 0)
        {
            Camera.main.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
    }

    public void ShakeCamera(float shakeDuration = 1)
    {
        this.shakeDuration = shakeDuration;
        originalPos = Camera.main.transform.localPosition;
    }

    public void ShakeCamera(float shakeDuration = 1, float shakeAmount = 0.7f, float decreaseFactor = 1)
    {
        this.shakeDuration = shakeDuration;
        this.shakeAmount = shakeAmount;
        this.decreaseFactor = decreaseFactor;
    }
}
