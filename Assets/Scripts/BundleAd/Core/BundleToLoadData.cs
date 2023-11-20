using UnityEngine;

namespace PesPatron.Bundles
{
    public class BundleToLoadData 
    {
        public readonly string BundleName;
        public readonly int Version;

        public BundleToLoadData(string bundleName, int version)
        {
            BundleName = bundleName;
            Version = version;
        }
    }
}