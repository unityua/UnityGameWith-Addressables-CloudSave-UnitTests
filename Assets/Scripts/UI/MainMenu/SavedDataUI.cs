using PesPatron.Core;
using PesPatron.Core.SaveLoad;
using PesPatron.Helpers;
using System.Text;
using TMPro;
using UnityEngine;

namespace PesPatron.UI
{
    public class SavedDataUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [Space]
        [SerializeField] private string _noneDataMessage = "None";
        [SerializeField] private string _notLoadedMessage = "No connection";

        private void Start()
        {
            bool dataLoaded = ProjectServices.GetService<GlobalGameData>().WebServicesInitialized;

            if (dataLoaded)
                _label.text = LoadSavedData(ProjectServices.GetService<ISaveLoadSystem>());
            else
                _label.text = _notLoadedMessage;
        }

        private string LoadSavedData(ISaveLoadSystem saveLoadSystem)
        {
            string[] allKeys = saveLoadSystem.AllStoredKeys;

            if (allKeys.Length == 0)
                return _noneDataMessage;

            StringBuilder result = new StringBuilder();

            foreach (var key in allKeys)
            {
                result.Append(key);
                result.Append(" - ");
                result.Append(TimeUtils.TimeFormatToMMSS(saveLoadSystem.Load(key)));
                result.Append("\n");
            }

            return result.ToString();
        }
    }
}