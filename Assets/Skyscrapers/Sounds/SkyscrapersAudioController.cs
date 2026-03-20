using UnityEngine;

public class SkyscrapersAudioController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform bridge;

    [Header("Audio sources")]
    [SerializeField] private AudioSource cityAmbiance;
    [Range(0f, 1f)]
    [SerializeField] private float cityAmbianceVolume;

    [SerializeField] private AudioSource trafficSound;
    [Range(0f, 1f)]
    [SerializeField] private float trafficSoundVolume;

    [SerializeField] private AudioSource normalWind;
    [Range(0f, 1f)]
    [SerializeField] private float normalWindVolume;

    [SerializeField] private AudioSource strongWind;
    [Range(0f, 1f)]
    [SerializeField] private float strongWindVolume;

    [Header("Threshold points")]
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;

    void Start()
    {
        cityAmbiance.Play();
        trafficSound.Play();
        normalWind.Play();
        strongWind.Play();
    }

    void Update()
    {
        if (player == null || bridge == null) return;

        float distance = Vector3.Distance(bridge.position, player.position);
        float t = 1f - Mathf.InverseLerp(minDistance, maxDistance, distance);

        cityAmbiance.volume = cityAmbianceVolume;
        strongWind.volume = t * strongWindVolume;
        normalWind.volume = (1f - t) * normalWindVolume;
        trafficSound.volume = (1f - t * 0.5f) * trafficSoundVolume;
    }

    void OnDrawGizmosSelected()
    {
        if (bridge == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(bridge.position, maxDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(bridge.position, minDistance);
    }
}
