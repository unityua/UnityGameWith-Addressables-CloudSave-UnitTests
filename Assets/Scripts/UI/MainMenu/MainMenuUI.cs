using PesPatron.Core;
using System.Collections.Generic;
using UnityEngine;


namespace PesPatron.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private LevelData _fieldLevel;
        [Header("UI")]
        [SerializeField] private WebLevelsUI _webLevelsUI;
        [SerializeField] private LoadLevelButton _loadFieldLevelButton;

        private List<LoadLevelButton> _allButtons = new List<LoadLevelButton>();

        private ISceneChanger _sceneChanger;

        private void Awake()
        {
            _sceneChanger = ProjectServices.GetService<ISceneChanger>();
            _webLevelsUI.Construct(ProjectServices.GetService<WebLevelsLoader>());
        }

        private void Start()
        {
            RegisterButton(_loadFieldLevelButton, _fieldLevel);

            _webLevelsUI.CreatedButton += RegisterButton;
            _webLevelsUI.Initialize();
        }

        private void TryLoadLevel(LevelData levelData)
        {
            SetAllButtonsEnabled(false);
            _sceneChanger.Load(levelData, () => SetAllButtonsEnabled(true));
        }

        private void RegisterButton(LoadLevelButton button, LevelData levelToLoad)
        {
            button.Construct(levelToLoad);
            button.Initialize();
            button.ButtonClicked += OnButtonClicked;
            _allButtons.Add(button);
        }

        private void OnButtonClicked(LoadLevelButton button, LevelData levelToLoad)
        {
            TryLoadLevel(levelToLoad);
        }

        private void SetAllButtonsEnabled(bool value)
        {
            foreach (var levelButton in _allButtons)
                levelButton.SetEnabled(value);
        }
    }
}