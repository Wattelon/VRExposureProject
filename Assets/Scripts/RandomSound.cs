using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomBirdSound : MonoBehaviour
{
    public float minDelay = 15f;   
    public float maxDelay = 30f;   

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayRandomBird());
    }

    System.Collections.IEnumerator PlayRandomBird()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            audioSource.Play();
        }
    }
}