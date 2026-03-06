using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModularScene
{
    public class ModularSceneLoader : MonoBehaviour
    {
        [SerializeField] private List<GameObject> sceneGameObjectsModules;
        [SerializeField] private Terrain terrain;

        private void Start()
        {
            var settings = ModularSceneSettingsManager.Load();
            
            
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