using PesPatron.Bundles;
using PesPatron.CameraStuff;
using PesPatron.Core.SaveLoad;
using PesPatron.PlayerStuff;
using PesPatron.UI;
using UnityEngine;

namespace PesPatron.Core
{
    public class LevelStarter : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private CameraFollow _cameraFollow;
        [SerializeField] private AnimalsCatchTarget _animalsCatchTarget;
        [SerializeField] private GameTimer _gameTimer;
        [SerializeField] private LevelBannerCreator _levelBannerCreator;
        [Space]
        [SerializeField] private MainGameUI _mainGameUI;
        [SerializeField] private Joystick _inputJoystick;
        [SerializeField] private PlayerJoystickInput _playerJoystickInput;

        private ISceneChanger _sceneChanger;
        private LevelDataProvider _levelDataProvider;
        private ISaveLoadSystem _saveLoadSystem;
        private IBundlesLoader _bundlesLoader;

        private void Awake()
        {
            GetServices();
            ConstructItems();
            InitializeItems();
            StartGame();
        }

        private void GetServices()
        {
            _bundlesLoader = ProjectServices.GetService<IBundlesLoader>();
            _levelDataProvider = ProjectServices.GetService<LevelDataProvider>();
            _sceneChanger = ProjectServices.GetService<ISceneChanger>();
            _saveLoadSystem = ProjectServices.GetService<ISaveLoadSystem>();
        }

        private void ConstructItems()
        {
            if (_levelBannerCreator == null)
                _levelBannerCreator = FindAnyObjectByType<LevelBannerCreator>();

            if (_levelBannerCreator != null)
                _levelBannerCreator.Construct(_bundlesLoader);
            _playerJoystickInput.Construct(_cameraFollow.MainCamera.transform, _inputJoystick, _player.Movement);
            _mainGameUI.Construct(_sceneChanger);
        }

        private void InitializeItems()
        {
            if (_levelBannerCreator)
                _levelBannerCreator.Initialize();
            _animalsCatchTarget.Initialize();
            _player.Initialize();
            _playerJoystickInput.Initialize();
            _mainGameUI.Initialize(_animalsCatchTarget);

            var newPlayerVisual = Instantiate(_levelDataProvider.LevelData.CharacterVisual);
            _player.SetVisual(newPlayerVisual);
        }

        private void StartGame()
        {
            _gameTimer.Restart();

            _animalsCatchTarget.AllTargetsCatched += _ => WinGame();
            _player.Health.Died += _ => LoseGame();

            _cameraFollow.SetTraget(_player.transform);
        }

        private void WinGame()
        {
            _gameTimer.Stop();

            _player.Health.Invincible = true;

            _inputJoystick.SetVisible(false);
            _player.DisableCharacter();

            SaveBestTime(Mathf.RoundToInt(_gameTimer.PassedTime));

            _mainGameUI.EndScreen.ShowWinScreen(_gameTimer.PassedTime);
        }

        private void LoseGame()
        {
            _gameTimer.Stop();

            _inputJoystick.SetVisible(false);
            _player.DisableCharacter();

            _mainGameUI.EndScreen.ShowLoseScreen(_gameTimer.PassedTime);
        }

        private void SaveBestTime(int playTimeSeconds)
        {
            string levelName = _levelDataProvider.LevelData.SceneName;

            if (_saveLoadSystem.HasData(levelName) == false || playTimeSeconds > _saveLoadSystem.Load(levelName))
                _saveLoadSystem.Save(levelName, playTimeSeconds);
        }
    }
}