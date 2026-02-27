using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace ModularScene
{
    public class ModularSceneLoader : MonoBehaviour
    {
        [SerializeField] private List<GameObject> sceneGameObjectsModules;
        [SerializeField] private Terrain terrain;

        private void Start()
        {
            var random = new Random();
            var settings = new ModularSceneSettings();//ModularSceneSettingsManager.Load();
            foreach (var sceneGameObjectModule in Enum.GetValues(typeof(SceneGameObjectModule)))
            {
                settings.SceneGameObjectModules.Add((SceneGameObjectModule)sceneGameObjectModule, random.Next(2) == 1);
            }
            foreach (var sceneDetailsModule in Enum.GetValues(typeof(SceneDetailsModule)))
            {
                settings.SceneDetailsModules.Add((SceneDetailsModule)sceneDetailsModule, random.Next(2) == 1);
            }
            settings.TreesModule = random.Next(2) == 1;
            
            
            for (var i = 0; i < sceneGameObjectsModules.Count; i++)
            {
                sceneGameObjectsModules[i].SetActive(settings.SceneGameObjectModules[(SceneGameObjectModule)i]);
            }
            
            if (settings.SceneDetailsModules.All(module => module.Value) && settings.TreesModule) return;

            terrain.terrainData = Instantiate(terrain.terrainData);
            
            if (!settings.TreesModule) terrain.terrainData.SetTreeInstances(Array.Empty<TreeInstance>(), false);
            
            if (settings.SceneDetailsModules.All(module => module.Value)) return;

            var detailPrototypesRemoved = 0;
            for (var i = 0; i < terrain.terrainData.detailPrototypes.Length; i++)
            {
                if (!settings.SceneDetailsModules.ContainsKey((SceneDetailsModule)i) || settings.SceneDetailsModules[(SceneDetailsModule)i]) continue;
                terrain.terrainData.RemoveDetailPrototype(i - detailPrototypesRemoved);
                detailPrototypesRemoved++;
            }
        }
    }
}