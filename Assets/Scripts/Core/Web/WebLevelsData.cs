using PesPatron.Helpers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace PesPatron.Core
{
    [CreateAssetMenu(fileName = "WebLevelsData Config", menuName = ScriptableObjectsPath.DATA + "WebLevelsData")]
    public class WebLevelsData : ScriptableObject
    {
        [SerializeField] private List<AssetReferenceT<LevelData>> _aviableLevels = new();

        public int LevelsCount => _aviableLevels.Count;

        public AssetReferenceT<LevelData> GetLevelDataReferenceByIndex(int index) => _aviableLevels[index];
    }
}