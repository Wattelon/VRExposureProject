using System;
using System.Collections.Generic;

namespace ModularScene
{
    [Serializable]
    public class ModularSceneSettings
    {
        public List<bool> SceneGameObjectModules;
        public bool SafeSpaceSceneModule = true;
        public bool TreesModule = true;
    }
}