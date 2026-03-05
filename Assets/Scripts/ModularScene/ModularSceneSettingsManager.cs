using UnityEngine;

namespace ModularScene
{
    public static class ModularSceneSettingsManager
    {
        private const string SaveKey = "ModularSceneSettings";

        public static void Save(ModularSceneSettings settings)
        {
            var json = JsonUtility.ToJson(settings);
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }

        public static ModularSceneSettings Load()
        {
            if (!PlayerPrefs.HasKey(SaveKey)) return new ModularSceneSettings();

            var json = PlayerPrefs.GetString(SaveKey);
            return JsonUtility.FromJson<ModularSceneSettings>(json);
        }
    }
}