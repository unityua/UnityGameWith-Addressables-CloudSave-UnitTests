using PesPatron.Core;
using TMPro;
using UnityEngine;

namespace PesPatron.UI
{
    public class LoadingLevelsLabel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textLabel;
        [Space]
        [SerializeField] private string _loadingLevels = "Loading Levels";
        [SerializeField] private string _allLevelsLoaded = "All Levels Loaded";
        [SerializeField] private string _loadFailed = "Can't Load All Levels";

        private WebLevelsLoader _webLevelsLoader;

        private void Awake()
        {
            _webLevelsLoader = ProjectServices.GetService<WebLevelsLoader>();

            if (_webLevelsLoader.LoadingCompleted)
            {
                OnAllWebLevelsLoaded(_webLevelsLoader);
            }
            else
            {
                SetLabel(_loadingLevels);
                _webLevelsLoader.AllWebLevelsLoaded += OnAllWebLevelsLoaded;
            }
        }

        private void OnAllWebLevelsLoaded(WebLevelsLoader sender)
        {
            if (sender.WebLevelsDataDownloaded && sender.LoadedLevelsCount == sender.TotalWebLevelsCount)
                SetLabel(_allLevelsLoaded);
            else
                SetLabel(_loadFailed);
        }

        private void SetLabel(string message)
        {
            _textLabel.text = message;
        }
    }
}