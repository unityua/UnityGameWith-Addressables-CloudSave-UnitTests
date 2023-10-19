using PesPatron.Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PesPatron.UI
{
    public class LoadLevelButton : MonoBehaviour
    {
        [SerializeField] private LevelData _levelToLoad;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _textLabel;

        public event Action<LoadLevelButton, LevelData> ButtonClicked;

        private void OnDestroy()
        {
            ButtonClicked = null;
        }

        public void Construct(LevelData levelData)
        {
            _levelToLoad = levelData;
            _textLabel.text = levelData.FancyLevelName;
        }

        public void Initialize()
        {
            _button.onClick.AddListener(() => ButtonClicked?.Invoke(this, _levelToLoad));
        }

        public void SetEnabled(bool value)
        {
            _button.enabled = value;
        }
    }
}