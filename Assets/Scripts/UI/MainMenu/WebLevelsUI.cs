using PesPatron.Core;
using System;
using UnityEngine;

namespace PesPatron.UI
{
    public class WebLevelsUI : MonoBehaviour
    {
        [SerializeField] private LoadLevelButton _buttonPrefab;
        [SerializeField] private Transform _buttonsParent;

        private WebLevelsLoader _webLevelsLoader;

        public event Action<LoadLevelButton, LevelData> CreatedButton;

        public void Construct(WebLevelsLoader webLevelsLoader)
        {
            _webLevelsLoader = webLevelsLoader;
        }

        public void Initialize()
        {
            if (_webLevelsLoader.LoadingCompleted == false)
                _webLevelsLoader.LoadedLevel += LoadedNewLevel;

            CreateLoadedButtons();
        }

        private void OnDestroy()
        {
            _webLevelsLoader.LoadedLevel -= LoadedNewLevel;
        }

        private void LoadedNewLevel(WebLevelsLoader sender, LevelData loadedLevel)
        {
            CreateButton(loadedLevel);
        }

        private void CreateLoadedButtons()
        {
            for (int i = 0; i < _webLevelsLoader.LoadedLevelsCount; i++)
            {
                CreateButton(_webLevelsLoader.GetLoadedLevelData(i));
            }
        }

        private void CreateButton(LevelData levelData)
        {
            LoadLevelButton newButton = Instantiate(_buttonPrefab, _buttonsParent);

            CreatedButton?.Invoke(newButton, levelData);
        }
    }
}