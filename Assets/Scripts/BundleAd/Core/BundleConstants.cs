using UnityEngine;

namespace PesPatron.Bundles
{
    public static class BundleConstants
    {
        public const string AD_BUNDLE_KEY = "adbundle";
        public const string AD_CONTAINER_BUNDLE_NAME = "BundleAdContainer";

        public const string WEB_BUNDLES_PATH = "https://storage.googleapis.com/pespatron/bundles/";

        public const string STANDALONE_WINDOWS_WEB_PATH = WEB_BUNDLES_PATH + "StandaloneWindows/";
        public const string ANDROID_WEB_PATH = WEB_BUNDLES_PATH + "Android/";
    }
}