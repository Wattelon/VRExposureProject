using UnityEngine;

public class BreakGlassMaterial : MonoBehaviour
{
    [SerializeField] private Material brokenGlassMaterial;
    [Tooltip("Index of the glass material in the Mesh Renderer.")]
    [SerializeField] private int glassMaterialIndex;

    [SerializeField] private AudioClip[] breakingGlassSounds;

    private MeshRenderer meshRenderer;
    private AudioSource audioSource;
    private bool isBroken = false;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isBroken) return;
        isBroken = true;

        if (meshRenderer == null || brokenGlassMaterial == null) return;
        Material[] mats = meshRenderer.materials;
        mats[glassMaterialIndex] = brokenGlassMaterial;
        meshRenderer.materials = mats;

        if (audioSource == null || breakingGlassSounds == null) return;
        audioSource.PlayOneShot(breakingGlassSounds[Random.Range(0, breakingGlassSounds.Length)]);
    }
}
