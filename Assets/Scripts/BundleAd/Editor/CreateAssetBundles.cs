using System.IO;
using UnityEditor;

namespace PesPatron.Bundles
{
    public class CreateAssetBundles
    {
        public const string BUNDLES_PATH = "Assets/AssetBundles Stuff/Builded Bundles";

        [MenuItem("Tools/Build AssetBundles")]
        private static void BuildAllAssetBundles()
        {
            BuildAssetBundlesFor(BuildTarget.Android.ToString(), BuildTarget.Android);
            BuildAssetBundlesFor(BuildTarget.StandaloneWindows.ToString(), BuildTarget.StandaloneWindows);
        }

        private static void BuildAssetBundlesFor(string folderName, BuildTarget buildTarget)
        {
            string finalPath = BUNDLES_PATH + "/" + folderName;

            if (AssetDatabase.IsValidFolder(finalPath) == false)
            {
                AssetDatabase.CreateFolder(BUNDLES_PATH, folderName);
            }

            BuildPipeline.BuildAssetBundles(finalPath, BuildAssetBundleOptions.None, buildTarget);
        }
    }
}