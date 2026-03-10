using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace ModularScene
{
    public class ModularSceneLoader : MonoBehaviour
    {
        [SerializeField] private List<GameObject> sceneGameObjectsModules;
        [SerializeField] private Terrain terrain;
        [SerializeField] private Camera centerCamera;

        private void Start()
        {
            var settings = ModularSceneSettingsManager.Load();
            
            for (var i = 0; i < sceneGameObjectsModules.Count; i++)
            {
                sceneGameObjectsModules[i].SetActive(settings.SceneGameObjectModules[i]);
            }

            centerCamera.GetUniversalAdditionalCameraData().renderPostProcessing = settings.SafeSpaceSceneModule;
            
            if (settings.TreesModule) return;
            terrain.terrainData = Instantiate(terrain.terrainData);
            terrain.terrainData.SetTreeInstances(Array.Empty<TreeInstance>(), false);
        }
    }
}