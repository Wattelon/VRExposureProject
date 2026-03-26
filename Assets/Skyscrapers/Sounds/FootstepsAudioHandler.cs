using System.Collections.Generic;
using UnityEngine;

public class FootstepsAudioHandler : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float stepDistance = 1.5f;
    [SerializeField] private float raycastLength = 2f;
    [SerializeField] private float pitchVariation = 0.2f;

    [System.Serializable]
    public struct SurfaceAudioMapping
    {
        public string name;
        public PhysicsMaterial physicsMaterial;
        public AudioClip[] audioClips;
    }
    [SerializeField] private List<SurfaceAudioMapping> audioMappings;

    private Vector3 previousStep;
    private float distanceTravelled = 0;

    private void Start()
    {
        previousStep = player.position;
    }

    private void Update()
    {
        distanceTravelled += Vector3.Distance(player.position, previousStep);
        previousStep = player.position;

        if (distanceTravelled < stepDistance) return;

        PlayFootstep();
        distanceTravelled = 0;
    }

    private void PlayFootstep()
    {
        var rayStart = transform.position + Vector3.up * 0.5f;
        Debug.DrawRay(rayStart, Vector3.down * raycastLength, Color.red, 1f);

        if (Physics.Raycast(rayStart, Vector3.down, out var hit, raycastLength))
        {
            var surfaceMat = hit.collider.sharedMaterial;
            if (surfaceMat == null) return;

            AudioClip[] clips = null;
            foreach (var mapping in audioMappings)
            {
                if (mapping.physicsMaterial == surfaceMat)
                {
                    clips = mapping.audioClips;
                    break;
                }
            }
            if (clips == null || clips.Length == 0) return;

            audioSource.pitch = Random.Range(1f - pitchVariation / 2, 1f + pitchVariation / 2);
            audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
    }
}
