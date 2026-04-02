using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ShadowDistanceController : MonoBehaviour
{
    [SerializeField] private float sceneShadowDistance = 50f;
    private float defaultShadowDistance;

    void OnEnable()
    {
        if (GraphicsSettings.currentRenderPipeline is UniversalRenderPipelineAsset urpAsset)
        {
            defaultShadowDistance = urpAsset.shadowDistance;
            urpAsset.shadowDistance = sceneShadowDistance;
        }
    }

    void OnDisable()
    {
        if (GraphicsSettings.currentRenderPipeline is UniversalRenderPipelineAsset urpAsset)
        {
            urpAsset.shadowDistance = defaultShadowDistance;
        }
    }
}
