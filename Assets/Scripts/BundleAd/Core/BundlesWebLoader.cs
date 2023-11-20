using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PesPatron.Bundles
{
    public class BundlesWebLoader : MonoBehaviour, IBundlesLoader
    {
        private Dictionary<string, AssetBundle> _loadedBundles = new Dictionary<string, AssetBundle>();

        private BundleToLoadData[] _bundlesToLoad = new BundleToLoadData[]
        {
            new BundleToLoadData(BundleConstants.AD_BUNDLE_KEY, 0),
        };

        public event Action<string, AssetBundle> LoadedAssetBundle;

        public void LoadAllBundles()
        {
            string bundlesFolderWebPath;

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    bundlesFolderWebPath = BundleConstants.STANDALONE_WINDOWS_WEB_PATH;
                    break;
                case RuntimePlatform.Android:
                    bundlesFolderWebPath = BundleConstants.ANDROID_WEB_PATH;
                    break;
                default:
                    throw new NotImplementedException($"Not Implemented Bundles For {Application.platform} Platform");
            }

            foreach (var bundleToLoad in _bundlesToLoad)
            {
                StartCoroutine(LoadBundle(bundlesFolderWebPath, bundleToLoad.BundleName, bundleToLoad.Version));
            }
        }

        public bool BundleIsLoaded(string bundleName)
        {
            return _loadedBundles.ContainsKey(bundleName);
        }

        public AssetBundle GetLoadedBundle(string bundleName)
        {
            return _loadedBundles[bundleName];
        }

        private IEnumerator LoadBundle(string webPath, string bundleName, int version)
        {
            while (Caching.ready == false)
                yield return null;

            WWW loadedBundle = WWW.LoadFromCacheOrDownload(webPath + bundleName, version);

            yield return loadedBundle;

            bool succesfull = string.IsNullOrEmpty(loadedBundle.error);

            if (succesfull)
            {
                _loadedBundles.Add(bundleName, loadedBundle.assetBundle);
                LoadedAssetBundle?.Invoke(bundleName, loadedBundle.assetBundle);

                Debug.Log("Loaded Bundle Successfully! Bundle Name: " + bundleName);
            }
            else
            {
                Debug.LogError($"Error while loading asset bundle '{bundleName}'. Error Message " + loadedBundle.error);
            }
        }
    }
}