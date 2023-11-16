using System;
using UnityEngine;

namespace PesPatron.Bundles
{
    public interface IBundlesLoader
    {
        event Action<string, AssetBundle> LoadedAssetBundle;

        public bool BundleIsLoaded(string bundleName);

        public AssetBundle GetLoadedBundle(string bundleName);
    }
}