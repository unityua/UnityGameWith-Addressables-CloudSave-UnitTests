using System;
using System.Collections.Generic;
using UnityEngine;

namespace PesPatron.Bundles
{
    public class BundlesWebLoader : MonoBehaviour, IBundlesLoader
    {
        private Dictionary<string, AssetBundle> _loadedBundles = new Dictionary<string, AssetBundle>();

        public event Action<string, AssetBundle> LoadedAssetBundle;

        public void LoadAllBundles()
        {
            //TODO: Load Bundles :)
        }

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