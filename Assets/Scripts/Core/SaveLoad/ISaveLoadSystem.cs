using System.Threading.Tasks;
using UnityEngine;

namespace PesPatron.Core.SaveLoad
{
    public interface ISaveLoadSystem
    {
        Task LoadAllData();
        void Save(string key, int value);
        bool HasData(string key);
        string LoadRaw(string key);
        int Load(string key);
        void SaveUsernameAndPassword(string username, string password);

        string[] AllStoredKeys { get; }
        string StoredUsername { get; }
        string StoredPassword { get; }
    }
}