using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomLoopOffset : MonoBehaviour
{
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        audioSource.time = Random.Range(0f, audioSource.clip.length);
        audioSource.Play();
    }
}