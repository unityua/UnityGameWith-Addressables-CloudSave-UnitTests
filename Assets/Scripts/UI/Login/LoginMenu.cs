using System;
using TMPro;
using UnityEngine;

namespace PesPatron.UI
{
    public class LoginMenu : MonoBehaviour
    {
        [SerializeField] private SelectOptionScreen _selectOptionsScreen;
        [SerializeField] private LoginScreen _loginScreen;
        [Space]
        [SerializeField] private TMP_Text _messageLabel;
        [Space]
        [SerializeField] private string _signUpButtonText = "SIGN UP";
        [SerializeField] private string _loginButtonText = "LOGIN";

        private string _storedUsername;
        private string _storedPassword;

        private bool _inLoginMode;

        private ScreenBase[] _allScreens;

        public event Action SkipButtonPressed;
        public event Action<string, string> LoginButtonPressed;
        public event Action<string, string> SignUpButtonPressed;


        public void Initialize(string storedUsername, string storedPassword)
        {
            _allScreens = new ScreenBase[]
            {
                _selectOptionsScreen,
                _loginScreen
            };

            foreach (var screen in _allScreens)
            {
                screen.Initialize();
            }

            _storedUsername = storedUsername;
            _storedPassword = storedPassword;

            _selectOptionsScreen.LoginButtonPressed += ShowLoginScreen;
            _selectOptionsScreen.SignUpButtonPressed += ShowSignUpScreen;
            _selectOptionsScreen.SkipButtonPressed += LoginAnonymous;


            _loginScreen.ApplyButtonPressed += LoginApplyButtonPressed;
            _loginScreen.BackButtonPressed += ShowSelectOptionsScreen;

            ShowScreen(_selectOptionsScreen);
        }

        public void SetInteractablesEnabled(bool value)
        {
            foreach (var screen in _allScreens)
                screen.SetInteractablesEnabled(value);
        }

        public void ShowMessage(string message, Color color)
        {
            _messageLabel.text = message;
            _messageLabel.color = color;
            _messageLabel.gameObject.SetActive(true);
        }

        public void HideMessage()
        {
            _messageLabel.gameObject.SetActive(false);
        }

        private void LoginApplyButtonPressed()
        {
            SetInteractablesEnabled(false);

            if (_inLoginMode)
                LoginButtonPressed?.Invoke(_loginScreen.EnteredUsername, _loginScreen.EnteredPassword);
            else
                SignUpButtonPressed?.Invoke(_loginScreen.EnteredUsername, _loginScreen.EnteredPassword);
        }

        private void LoginAnonymous()
        {
            SetInteractablesEnabled(false);
            SkipButtonPressed?.Invoke();
        }

        private void ShowSelectOptionsScreen()
        {
            ShowScreen(_selectOptionsScreen);
        }

        private void ShowLoginScreen()
        {
            _inLoginMode = true;
            _loginScreen.SetData(_loginButtonText, _storedUsername, _storedPassword);
            ShowScreen(_loginScreen);
        }

        private void ShowSignUpScreen()
        {
            _inLoginMode = false;
            _loginScreen.SetData(_signUpButtonText, string.Empty, string.Empty);
            ShowScreen(_loginScreen);
        }

        private void ShowScreen(ScreenBase screenToShow)
        {
            foreach (var screen in _allScreens)
                screen.Hide();

            screenToShow.Show();
        }

        internal void Initialize(string storedUsername, object storedPassword)
        {
            throw new NotImplementedException();
        }
    }
}