using UnityEngine;
using System.Collections;

public class BreathingTargets : MonoBehaviour
{
    [Header("Targets to scale (Circle НЕ добавляй сюда)")]
    public Transform innerPat;
    public Transform glow;

    [Header("4-7-8 timing")]
    public float inhaleTime = 4f;
    public float holdTime = 7f;
    public float exhaleTime = 8f;

    [Header("Scale")]
    public float scaleMultiplierMin = 1f;
    public float scaleMultiplierMax = 1.5f;

    Vector3 innerBaseScale;
    Vector3 glowBaseScale;

    void Awake()
    {
        if (innerPat != null) innerBaseScale = innerPat.localScale;
        if (glow != null) glowBaseScale = glow.localScale;
    }

    void Start()
    {
        StartCoroutine(Cycle());
    }

    IEnumerator Cycle()
    {
        while (true)
        {
            yield return ScaleBoth(scaleMultiplierMin, scaleMultiplierMax, inhaleTime);
            
            ApplyMultiplier(scaleMultiplierMax);
            yield return new WaitForSeconds(holdTime);
            
            yield return ScaleBoth(scaleMultiplierMax, scaleMultiplierMin, exhaleTime);
        }
    }

    IEnumerator ScaleBoth(float from, float to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            float k = Mathf.Lerp(from, to, t / duration);
            ApplyMultiplier(k);

            t += Time.deltaTime;
            yield return null;
        }
        ApplyMultiplier(to);
    }

    void ApplyMultiplier(float k)
    {
        if (innerPat != null) innerPat.localScale = innerBaseScale * k;
        if (glow != null) glow.localScale = glowBaseScale * k;
    }
}