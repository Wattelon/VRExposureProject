using UnityEngine;
using UnityEngine.SceneManagement;

public class ExposureFinish : MonoBehaviour
{
    private const string SafeSpaceSceneName = "SafeSpace";
    
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(SafeSpaceSceneName);
    }
}
