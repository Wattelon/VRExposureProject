using System.Collections.Generic;
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
        }

        private void OnDisable()
        {
            confirmButton.onClick.RemoveListener(OnConfirmButtonClicked);
        }

        private void LoadPreviousSettings()
        {
            var settings = ModularSceneSettingsManager.Load();

            safeSpaceSceneToggle.isOn = settings.SafeSpaceSceneModule;
            treesToggle.isOn = settings.TreesModule;

            foreach (var gameObjectModule in settings.SceneGameObjectModules)
            {
                gameObjectModulesToggles[(int)gameObjectModule.Key].isOn = gameObjectModule.Value;
            }
        }

        private void OnConfirmButtonClicked()
        {
            var settings = new ModularSceneSettings
            {
                SafeSpaceSceneModule = safeSpaceSceneToggle.isOn,
                TreesModule = treesToggle.isOn
            };

            for (var i = 0; i < gameObjectModulesToggles.Count; i++)
            {
                settings.SceneGameObjectModules.Add((SceneGameObjectModule)i, gameObjectModulesToggles[i].isOn);
            }
            
            ModularSceneSettingsManager.Save(settings);
            SceneManager.LoadScene("SafeSpace");
        }
    }
}