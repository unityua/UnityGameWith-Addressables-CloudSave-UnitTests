using System;
using UnityEngine;

namespace PesPatron.Bundles
{
    public interface IBundlesLoader
    {
        event Action<string, AssetBundle> LoadedAssetBundle;

        void LoadAllBundles();

        bool BundleIsLoaded(string bundleName);

        AssetBundle GetLoadedBundle(string bundleName);
    }
}