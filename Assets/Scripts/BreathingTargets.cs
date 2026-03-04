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

    [Header("Scale multipliers")]
    public float scaleMultiplierMin = 1f;
    public float scaleMultiplierMax = 1.5f;

    [Tooltip("Насколько уменьшится на задержке")]
    public float holdDipMultiplier = 0.97f;

    [Tooltip("За сколько секунд сделать небольшое уменьшение на задержке")]
    public float holdDipTime = 0.6f;

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
            // ВДОХ: min -> max
            yield return ScaleBoth(scaleMultiplierMin, scaleMultiplierMax, inhaleTime);

            // ЗАДЕРЖКА: чуть-чуть "осесть" и держать
            float holdTarget = scaleMultiplierMax * holdDipMultiplier;
            float dipT = Mathf.Min(holdDipTime, holdTime);
            yield return ScaleBoth(scaleMultiplierMax, holdTarget, dipT);

            float remainHold = holdTime - dipT;
            if (remainHold > 0f)
            {
                ApplyMultiplier(holdTarget);
                yield return new WaitForSeconds(remainHold);
            }

            // ВЫДОХ: holdTarget -> min
            yield return ScaleBoth(holdTarget, scaleMultiplierMin, exhaleTime);
        }
    }

    IEnumerator ScaleBoth(float from, float to, float duration)
    {
        if (duration <= 0f)
        {
            ApplyMultiplier(to);
            yield break;
        }

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