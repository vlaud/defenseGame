using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defense
{
    public static class ResourcesLoader
    {
        static Dictionary<string, Object> _resourceCache = new Dictionary<string, Object>();

        public static T Load<T>(string path) where T : Object
        {
            if (!_resourceCache.ContainsKey(path))
            {
                Object loadObject = Resources.Load<T>(path);
                if (loadObject == null)
                {
                    Debug.LogErrorFormat("{0} not loaded", path);
                    return null;
                }
                
                _resourceCache[path] = loadObject;                
            }

            return (T)_resourceCache[path];
        }

        public static void ReleaseAll()
        {
            _resourceCache.Clear();
        }

        public static void UnloadAsset(Object assetToUnload)
        {
            Resources.UnloadAsset(assetToUnload);
        }

        public static void UnloadUnusedAssets()
        {
            Resources.UnloadUnusedAssets();
        }
    }
}
