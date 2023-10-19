using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PesPatron.UI
{
    public class LoginScreen : ScreenBase
    {
        [SerializeField] private TMP_InputField _usernameInput;
        [SerializeField] private TMP_InputField _passwordInput;
        [Space]
        [SerializeField] private Button _applyButton;
        [SerializeField] private TMP_Text _applyLabel;
        [Space]
        [SerializeField] private Button _backButton;

        public string EnteredUsername => _usernameInput.text;
        public string EnteredPassword => _passwordInput.text;

        public event Action ApplyButtonPressed;
        public event Action BackButtonPressed;

        private void OnDestroy()
        {
            ApplyButtonPressed = null;
            BackButtonPressed = null;
        }

        public override void Initialize()
        {
            _applyButton.onClick.AddListener(() => ApplyButtonPressed?.Invoke());
            _backButton.onClick.AddListener(() => BackButtonPressed?.Invoke());
        }

        public void SetData(string applyButtonText, string username, string password)
        {
            _applyLabel.text = applyButtonText;

            _usernameInput.text = username;
            _passwordInput.text = password;
        }

        public override void SetInteractablesEnabled(bool value)
        {
            _usernameInput.enabled = value;
            _passwordInput.enabled = value;
            _applyButton.enabled = value;
            _backButton.enabled = value;
        }
    }
}