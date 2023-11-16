using System;
using System.Collections.Generic;
using UnityEngine;

namespace PesPatron.Bundles
{
    public class BundlesLoader : MonoBehaviour, IBundlesLoader
    {
        private Dictionary<string, AssetBundle> _loadedBundles = new Dictionary<string, AssetBundle>();

        public event Action<string, AssetBundle> LoadedAssetBundle;

        public bool BundleIsLoaded(string bundleName)
        {
            return _loadedBundles.ContainsKey(bundleName);
        }

        public AssetBundle GetLoadedBundle(string bundleName) 
        {
            return _loadedBundles[bundleName];
        }
    }
}