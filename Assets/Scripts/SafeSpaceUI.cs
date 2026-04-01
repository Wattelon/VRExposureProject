using UnityEngine;
using UnityEngine.UI;

public class SafeSpaceUI : MonoBehaviour
{
    [SerializeField] private Color inactiveColor;
    
    public void LoadExposureScene(string sceneName)
    {
        SceneTransitionManager.Instance.TransitionToScene(sceneName);
    }

    public void ToggleMeditation(GameObject meditation)
    {
        meditation.SetActive(!meditation.activeSelf);
    }

    public void ToggleMeditationColor(Image toggleImage)
    {
        toggleImage.color = toggleImage.color == inactiveColor ? Color.white : inactiveColor;
    }
}
