using System;
using System.Collections.Generic;

namespace ModularScene
{
    [Serializable]
    public class ModularSceneSettings
    {
        public Dictionary<SceneGameObjectModule, bool> SceneGameObjectModules = new();
        public Dictionary<SceneDetailsModule, bool> SceneDetailsModules = new();
        public bool TreesModule;
    }
}