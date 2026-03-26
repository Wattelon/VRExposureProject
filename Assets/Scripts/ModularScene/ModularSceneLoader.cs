using System;
using System.Collections.Generic;
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
                sceneGameObjectsModules[i].SetActive(settings.SceneGameObjectModules[i]);
            }

            if (settings.TreesModule) return;
            terrain.terrainData = Instantiate(terrain.terrainData);
            terrain.terrainData.SetTreeInstances(Array.Empty<TreeInstance>(), false);
        }
    }
}