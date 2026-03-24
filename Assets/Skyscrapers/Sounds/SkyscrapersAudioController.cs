using System;
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

    [Header("Walking sounds")]
    [SerializeField] private AudioSource stepsSource;
    [Range(0f, 1f)]
    [SerializeField] private float stepsSourceVolume;

    [SerializeField] private float stepDistance;

    [SerializeField] private PhysicsMaterial concretePhysicsMaterial;
    [SerializeField] private AudioClip[] concreteStepClips;

    [SerializeField] private PhysicsMaterial glassPhysicsMaterial;
    [SerializeField] private AudioClip[] glassStepClips;

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

        updateSoundAmbiance();
        footstepsHandler();
    }

    void updateSoundAmbiance()
    {
        float distance = Vector3.Distance(bridge.position, player.position);
        float t = 1f - Mathf.InverseLerp(minDistance, maxDistance, distance);

        strongWind.volume = t * strongWindVolume;
        normalWind.volume = (1f - t) * normalWindVolume;
        trafficSound.volume = (1f - t * 0.5f) * trafficSoundVolume;
        cityAmbiance.volume = cityAmbianceVolume;

        stepsSource.volume = stepsSourceVolume;
    }

    private Vector3 previousStep;
    private float distanceTravelled = 0;
    private void footstepsHandler()
    {
        Vector3 playerPos = new Vector3(player.position.x, 0, player.position.z);
        Vector3 previousPos = new Vector3(previousStep.x, 0, previousStep.z);

        distanceTravelled += Vector3.Distance(playerPos, previousPos);
        previousStep = player.position;

        if (distanceTravelled < stepDistance) return;

        PlayFootstep(2f);
        distanceTravelled = 0;
    }

    private void PlayFootstep(float raycastLength)
    {
        RaycastHit hit;
        if (Physics.Raycast(player.position, Vector3.down, out hit, raycastLength))
        {
            PhysicsMaterial surfacePhysicsMaterial = hit.collider.sharedMaterial;
            if (surfacePhysicsMaterial == null) return;

            AudioClip[] clips = null;
            if (surfacePhysicsMaterial == concretePhysicsMaterial)
                clips = concreteStepClips;
            else if (surfacePhysicsMaterial == glassPhysicsMaterial)
                clips = glassStepClips;

            stepsSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            stepsSource.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Length)]);
        }
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
