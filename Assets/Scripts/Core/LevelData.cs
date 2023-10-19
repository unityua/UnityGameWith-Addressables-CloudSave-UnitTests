using PesPatron.Characters;
using PesPatron.Helpers;
using UnityEngine;

namespace PesPatron.Core
{
    [CreateAssetMenu(fileName = "New Level Data", menuName = ScriptableObjectsPath.DATA + "LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private string _levelName;
        [SerializeField] private CharacterVisual _characterVisual;
        [Space]
        [SerializeField] private bool _isAdressable;
        [SerializeField] private string _sceneName;

        public string FancyLevelName => _levelName; 
        public CharacterVisual CharacterVisual => _characterVisual; 
        public bool IsAdressable => _isAdressable;
        public string SceneName => _sceneName;
    }
}