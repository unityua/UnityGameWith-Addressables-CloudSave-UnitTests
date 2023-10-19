using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PesPatron.Core
{
    public class WebLevelsLoader
    {
        private readonly AssetReferenceT<WebLevelsData> _webLevelsDataReference;
        private readonly List<LevelData> _loadedLevels = new List<LevelData>();

        private bool _loadingCompleted;
        private WebLevelsData _webLevelsData;
        private bool _webLevelsDataDownloaded;

        private int _loadedLevelsCount;
        private int _totalWebLevelsCount;

        public bool LoadingCompleted => _loadingCompleted;
        public bool WebLevelsDataDownloaded => _webLevelsDataDownloaded;
        public int LoadedLevelsCount => _loadedLevels.Count;
        public int TotalWebLevelsCount => _totalWebLevelsCount;


        public event Action<WebLevelsLoader, LevelData> LoadedLevel;
        public event Action<WebLevelsLoader> AllWebLevelsLoaded;

        public WebLevelsLoader(AssetReferenceT<WebLevelsData> webLevelsReference)
        {
            _webLevelsDataReference = webLevelsReference;
        }

        public LevelData GetLoadedLevelData(int index)
        {
            return _loadedLevels[index];
        }

        public void LoadLevels()
        {
            var loadOperation = _webLevelsDataReference.LoadAssetAsync();

            loadOperation.Completed += OnWebLevelsLoadCompleted;
        }

        private void LoadAllLevelsData()
        {
            _loadedLevelsCount = 0;
            _totalWebLevelsCount = _webLevelsData.LevelsCount;

            for (int i = 0; i < _totalWebLevelsCount; i++)
            {
                var asyncOperation = _webLevelsData.GetLevelDataReferenceByIndex(i).LoadAssetAsync();
                asyncOperation.Completed += OnLevelDataLoadCompleted;
            }
        }

        private void OnLevelDataLoadCompleted(AsyncOperationHandle<LevelData> levelDataOperation)
        {
            _loadedLevelsCount += 1;

            if(levelDataOperation.Status == AsyncOperationStatus.Succeeded)
            {
                _loadedLevels.Add(levelDataOperation.Result);

                LoadedLevel?.Invoke(this, levelDataOperation.Result);
            }

            if(_loadedLevelsCount == _webLevelsData.LevelsCount)
            {
                InvokeAllLevelsLoaded();
            }
        }

        private void OnWebLevelsLoadCompleted(AsyncOperationHandle<WebLevelsData> operation)
        {
            if(operation.Status == AsyncOperationStatus.Succeeded)
            {
                _webLevelsDataDownloaded = true;
                _webLevelsData = operation.Result;
                LoadAllLevelsData();
            }
            else
            {
                InvokeAllLevelsLoaded();
            }
        }

        private void InvokeAllLevelsLoaded()
        {
            _loadingCompleted = true;
            AllWebLevelsLoaded?.Invoke(this);
        }
    }
}