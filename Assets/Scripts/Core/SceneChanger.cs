using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace PesPatron.Core
{
    public class SceneChanger : ISceneChanger
    {
        private readonly LevelDataProvider _levelDataProvider;
        private bool _loadingLevel;

        public SceneChanger(LevelDataProvider levelDataProvider)
        {
            _levelDataProvider = levelDataProvider;
        }

        public void Load(LevelData levelData, Action loadFailedAction)
        {
            if (_loadingLevel)
                return;

            _levelDataProvider.LevelData = levelData;

            if (levelData.IsAdressable)
            {
                Addressables.LoadSceneAsync(levelData.SceneName, LoadSceneMode.Single).Completed += 
                    result => OlLevelLoadCompleted(result, loadFailedAction);
            }
            else
            {
                SceneManager.LoadScene(levelData.SceneName);
                _loadingLevel = false;
            }
        }

        private void OlLevelLoadCompleted(AsyncOperationHandle<SceneInstance> result, Action loadFailedAction)
        {
            if(result.Status != AsyncOperationStatus.Succeeded)
            {
                loadFailedAction?.Invoke();
            }

            _loadingLevel = false;
        }

        public void LoadMainMenu()
        {
            Load(_levelDataProvider.MainMenuLevelData, null);
        }

        public void Reload()
        {
            Load(_levelDataProvider.LevelData, null);
        }
    }
}