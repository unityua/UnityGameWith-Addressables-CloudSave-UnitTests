
using System;

namespace PesPatron.Core
{
    public interface ISceneChanger
    {
        void Reload();
        void Load(LevelData levelData, Action loadFailedAction);
        void LoadMainMenu();
    }
}