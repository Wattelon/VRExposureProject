using UnityEngine;

public class BreathingPractice : MonoBehaviour
{
    [SerializeField] private AnimationCurve breathingCurve;
    [SerializeField] private Transform breathingSphere;

    private void Update()
    {
        breathingSphere.localScale = Vector3.one * breathingCurve.Evaluate(Time.time);
    }
}