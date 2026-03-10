using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ModularScene
{
    public class ModularSettingsUI : MonoBehaviour
    {
        [SerializeField] private Toggle safeSpaceSceneToggle;
        [SerializeField] private List<Toggle> gameObjectModulesToggles;
        [SerializeField] private Toggle treesToggle;
        [SerializeField] private Button confirmButton;

        private void Start()
        {
            LoadPreviousSettings();
        }

        private void OnEnable()
        {
            confirmButton.onClick.AddListener(OnConfirmButtonClicked);
            safeSpaceSceneToggle.onValueChanged.AddListener(OnSafeSpaceSceneToggleValueChanged);
        }

        private void OnDisable()
        {
            confirmButton.onClick.RemoveListener(OnConfirmButtonClicked);
            safeSpaceSceneToggle.onValueChanged.RemoveListener(OnSafeSpaceSceneToggleValueChanged);
        }

        private void LoadPreviousSettings()
        {
            var settings = ModularSceneSettingsManager.Load();

            safeSpaceSceneToggle.isOn = settings.SafeSpaceSceneModule;
            treesToggle.isOn = settings.TreesModule;

            for (var i = 0; i < settings.SceneGameObjectModules.Count; i++)
            {
                gameObjectModulesToggles[i].isOn = settings.SceneGameObjectModules[i];
            }
        }

        private void OnConfirmButtonClicked()
        {
            var settings = new ModularSceneSettings
            {
                SceneGameObjectModules = gameObjectModulesToggles.Select(toggle => toggle.isOn).ToList(),
                SafeSpaceSceneModule = safeSpaceSceneToggle.isOn,
                TreesModule = treesToggle.isOn
            };
            
            ModularSceneSettingsManager.Save(settings);
            SceneManager.LoadScene("SafeSpace");
        }
        
        private void OnSafeSpaceSceneToggleValueChanged(bool isOn)
        {
            foreach (var toggle in gameObjectModulesToggles)
            {
                toggle.interactable = isOn;
            }
            treesToggle.interactable = isOn;
        }
    }
}