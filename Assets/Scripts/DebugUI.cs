using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI frameCounter;

    private const float ExpSmoothingFactor = 0.9f;
    private const float RefreshFrequency = 0.5f;

    private float _timeSinceUpdate;
    private float _averageFps = 1f;

    private void Update()
    {
        _averageFps = ExpSmoothingFactor * _averageFps + (1f - ExpSmoothingFactor) * 1f / Time.unscaledDeltaTime;

        if (_timeSinceUpdate < RefreshFrequency)
        {
            _timeSinceUpdate += Time.deltaTime;
            return;
        }

        frameCounter.text = Mathf.RoundToInt(_averageFps).ToString();
        _timeSinceUpdate = 0f;
    }
}
