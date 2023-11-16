using PesPatron.Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace PesPatron.Bundles
{
    [CreateAssetMenu(fileName = nameof(BundleAdContainer), menuName = ScriptableObjectsPath.BUNDLES_DATA + nameof(BundleAdContainer))]
    public class BundleAdContainer : ScriptableObject
    {
        [SerializeField] private LevelBanner[] _bannerPrefabs;
        [SerializeField] private Material[] _bannerMaterials;
        
        public IReadOnlyList<LevelBanner> AllBannerPrefabs => _bannerPrefabs;
        public IReadOnlyList<Material> AllBannerMaterials => _bannerMaterials;
    }
}