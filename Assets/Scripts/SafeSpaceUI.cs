using UnityEngine;
using UnityEngine.SceneManagement;

public class SafeSpaceUI : MonoBehaviour
{
    public void LoadExposureScene(string sceneName)
    {
        SceneTransitionManager.Instance.TransitionToScene(sceneName);
    }

    public void ToggleMeditation(GameObject meditation)
    {
        meditation.SetActive(!meditation.activeSelf);
    }
}
