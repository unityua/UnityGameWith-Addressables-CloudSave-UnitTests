using PesPatron.Bundles;
using PesPatron.Core.SaveLoad;
using PesPatron.UI;
using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace PesPatron.Core
{
    public class GameStartUp : MonoBehaviour
    {
        [SerializeField] private LoginMenu _loginMenu;
        [Space]
        [SerializeField] private string _loginFailed = "Login Failed";
        [SerializeField] private string _signUpFailed = "Sign Up Failed!";

        private IBundlesLoader _bundlesLoader;
        private ISaveLoadSystem _saveLoadSystem;
        private ISceneChanger _sceneChanger;
        private GlobalGameData _globalGameData;
        private WebLevelsLoader _webLevelsLoader;

        private void Awake()
        {
            _bundlesLoader = ProjectServices.GetService<IBundlesLoader>();
            _saveLoadSystem = ProjectServices.GetService<ISaveLoadSystem>();
            _sceneChanger = ProjectServices.GetService<ISceneChanger>();
            _globalGameData = ProjectServices.GetService<GlobalGameData>();
            _webLevelsLoader = ProjectServices.GetService<WebLevelsLoader>();
        }

        private async void Start()
        {
            InitializeUI();

            _webLevelsLoader.LoadLevels();
            _bundlesLoader.LoadAllBundles();

            await TryDoServiceTask(UnityServices.InitializeAsync(), "Unity Services Initialization");

            _loginMenu.SetInteractablesEnabled(true);
        }

        private void InitializeUI()
        {
            _loginMenu.SignUpButtonPressed += TrySignUp;
            _loginMenu.LoginButtonPressed += TryLogin;
            _loginMenu.SkipButtonPressed += SkipLogin;

            _loginMenu.Initialize(_saveLoadSystem.StoredUsername, _saveLoadSystem.StoredPassword);
            _loginMenu.SetInteractablesEnabled(false);

            _loginMenu.HideMessage();
        }

        private async void TryLogin(string username, string password)
        {
            bool logged = await TryAuthentificate(LoginToServices(username, password), _loginFailed);

            if (logged)
            {
                _saveLoadSystem.SaveUsernameAndPassword(username, password);
                _sceneChanger.LoadMainMenu();
            }
        }

        private async void TrySignUp(string username, string password)
        {
            bool signedUp = await TryAuthentificate(SignUpToServices(username, password), _signUpFailed);

            if (signedUp)
            {
                _saveLoadSystem.SaveUsernameAndPassword(username, password);
                _sceneChanger.LoadMainMenu();
            }
        }

        private async void SkipLogin()
        {
            await TryAuthentificate(SignInAnonymous(), string.Empty);

            _sceneChanger.LoadMainMenu();
        }

        private async Task<bool> TryAuthentificate(Task<bool> authOperation, string failMessage) 
        {
            _loginMenu.HideMessage();
            _loginMenu.SetInteractablesEnabled(false);

            bool operationSuccessful = await authOperation;

            if (operationSuccessful)
            {
                _globalGameData.SetWebData(AuthenticationService.Instance.PlayerId);

                await _saveLoadSystem.LoadAllData();
                return true;
            }
            else
            {
                _loginMenu.SetInteractablesEnabled(true);
                _loginMenu.ShowMessage(failMessage, Color.red);
                return true;
            }
        }

        private async Task<bool> SignInAnonymous()
        {
            IAuthenticationService authService = AuthenticationService.Instance;

            return await TryDoServiceTask(authService.SignInAnonymouslyAsync(), "Anon Auth");
        }

        private async Task<bool> SignUpToServices(string username, string password)
        {
            IAuthenticationService authService = AuthenticationService.Instance;

            return await TryDoServiceTask(authService.SignUpWithUsernamePasswordAsync(username, password), "SignUp");
        }

        private async Task<bool> LoginToServices(string username, string password)
        {
            IAuthenticationService authService = AuthenticationService.Instance;

            return await TryDoServiceTask(authService.SignInWithUsernamePasswordAsync(username, password), "Login");
        }

        private async Task<bool> TryDoServiceTask(Task operation, string operationName)
        {
            try
            {
                await operation;
                Debug.Log($"{operationName} SUCCESFUL");
                return true;
            }
            catch (Exception ex)
            {
                Debug.Log($"{operationName} Failed :(");
                Debug.LogException(ex);
                return false;
            }
        }
    }
}