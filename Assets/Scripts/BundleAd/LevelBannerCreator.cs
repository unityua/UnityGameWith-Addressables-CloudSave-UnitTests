using PesPatron.Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace PesPatron.Bundles
{
    public class LevelBannerCreator : MonoBehaviour
    {
        [SerializeField] private Transform _bannerPointsParent;

        private IBundlesLoader _bundlesLoader;

        public void Construct(IBundlesLoader bundlesLoader)
        {
            _bundlesLoader = bundlesLoader;
        }

        public void Initialize()
        {
            if (_bundlesLoader.BundleIsLoaded(BundleConstants.AD_BUNDLE_KEY))
            {
                AssetBundle adBundle = _bundlesLoader.GetLoadedBundle(BundleConstants.AD_BUNDLE_KEY);
                CreateAllBanners(adBundle.LoadAsset<BundleAdContainer>(BundleConstants.AD_CONTAINER_BUNDLE_NAME));
            }
            else
            {
                _bundlesLoader.LoadedAssetBundle += OnAnyAssetBundleLoaded;
            }
        }

        private void OnAnyAssetBundleLoaded(string bundleName, AssetBundle assetBundle)
        {
            if (bundleName != BundleConstants.AD_BUNDLE_KEY)
                return;

            CreateAllBanners(assetBundle.LoadAsset<BundleAdContainer>(BundleConstants.AD_CONTAINER_BUNDLE_NAME));
        }

        private void CreateAllBanners(BundleAdContainer adContainer)
        {
            IReadOnlyList<LevelBanner> allPrefabs = adContainer.AllBannerPrefabs;
            IReadOnlyList<Material> allAdMaterials = adContainer.AllBannerMaterials;

            int pointsCount = _bannerPointsParent.childCount;
            for (int i = pointsCount - 1; i >= 0; i--)
            {
                Transform spawnPoint = _bannerPointsParent.GetChild(i);
                CreateBanner(spawnPoint, allPrefabs.RandomItem(), allAdMaterials.RandomItem());
            }
        }

        private void CreateBanner(Transform point, LevelBanner prefab, Material bannerMaterial)
        {
            LevelBanner instance = Instantiate(prefab, point.position, point.rotation, _bannerPointsParent);

            instance.SetAdMaterial(bannerMaterial);
        }
    }
}