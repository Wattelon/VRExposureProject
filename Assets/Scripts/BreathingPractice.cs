using UnityEngine;

public class BreathingPractice : MonoBehaviour
{
    [SerializeField] private AnimationCurve breathingCurve;
    [SerializeField] private Transform breathingSphere;
    [SerializeField] private float startingDelay;
    
    private float _currentDelay;
    private float _startTime;

    private void OnEnable()
    {
        _startTime = Time.time + startingDelay;
    }

    private void OnDisable()
    {
        _currentDelay = 0;
        breathingSphere.localScale = Vector3.one * breathingCurve.Evaluate(0);
    }

    private void Update()
    {
        _currentDelay += Time.deltaTime;
        if (_currentDelay < startingDelay) return;
        
        breathingSphere.localScale = Vector3.one * breathingCurve.Evaluate(Time.time - _startTime);
    }
}