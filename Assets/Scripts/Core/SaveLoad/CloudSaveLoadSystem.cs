using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using UnityEngine;

namespace PesPatron.Core.SaveLoad
{
    public class CloudSaveLoadSystem : ISaveLoadSystem
    {
        private Dictionary<string, string> _loadedData = new Dictionary<string, string>();
        private readonly GlobalGameData _globalGameData;

        private const string USERNAME_KEY = "username";
        private const string PASSWORD_KEY = "password"; 

        public string StoredUsername => PlayerPrefs.GetString(USERNAME_KEY, string.Empty);

        public string StoredPassword => PlayerPrefs.GetString(PASSWORD_KEY, string.Empty);

        public CloudSaveLoadSystem(GlobalGameData globalGameData)
        {
            _globalGameData = globalGameData;
        }

        public string[] AllStoredKeys => _loadedData.Keys.ToArray();

        public async Task LoadAllData()
        {
            _loadedData = await CloudSaveService.Instance.Data.LoadAllAsync();
        }

        public bool HasData(string key)
        {
            return _loadedData.ContainsKey(key);
        }

        public string LoadRaw(string key)
        {
            return _loadedData[key];
        }

        public int Load(string key)
        {
            return Convert.ToInt32(_loadedData[key]);
        }

        public void Save(string key, int value)
        {
            if (HasData(key))
                _loadedData[key] = value.ToString();
            else
                _loadedData.Add(key, value.ToString());

            if (_globalGameData.WebServicesInitialized)
                CloudSaveService.Instance.Data.ForceSaveAsync(ConvertLoadedDataToCloudDictionary());
        }

        public void SaveUsernameAndPassword(string username, string password)
        {
            PlayerPrefs.SetString(USERNAME_KEY, username);
            PlayerPrefs.SetString(PASSWORD_KEY, password);
        }

        private Dictionary<string, object> ConvertLoadedDataToCloudDictionary()
        {
            Dictionary<string, object> result = new Dictionary<string, object>(_loadedData.Count);

            foreach (var dataPair in _loadedData)
            {
                result.Add(dataPair.Key, dataPair.Value);
            }

            return result;
        }
    }
}