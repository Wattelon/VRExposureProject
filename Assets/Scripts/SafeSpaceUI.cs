using UnityEngine;
using UnityEngine.SceneManagement;

public class SafeSpaceUI : MonoBehaviour
{
    public void LoadExposureScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ToggleMeditation(GameObject meditation)
    {
        meditation.SetActive(!meditation.activeSelf);
    }
}
