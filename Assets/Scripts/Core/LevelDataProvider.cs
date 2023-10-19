using UnityEngine;

namespace PesPatron.Core
{
    public class LevelDataProvider : MonoBehaviour
    {
        [SerializeField] private LevelData _currentLevelData;
        [Space]
        [SerializeField] private LevelData _mainMenuLevelData;

        public LevelData LevelData { get => _currentLevelData; set => _currentLevelData = value; }
        public LevelData MainMenuLevelData => _mainMenuLevelData;
    }
}