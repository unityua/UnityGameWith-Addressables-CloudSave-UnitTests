using System;
using UnityEngine;
using UnityEngine.UI;

namespace PesPatron.UI
{
    public class SelectOptionScreen : ScreenBase
    {
        [SerializeField] private Button _loginButton;
        [SerializeField] private Button _signUpButton;
        [SerializeField] private Button _skipButton;

        public event Action LoginButtonPressed;
        public event Action SignUpButtonPressed;
        public event Action SkipButtonPressed;

        private void OnDestroy()
        {
            LoginButtonPressed = null;
            SignUpButtonPressed = null;
            SkipButtonPressed = null;
        }

        public override void Initialize()
        {
            _loginButton.onClick.AddListener(() => LoginButtonPressed?.Invoke());
            _signUpButton.onClick.AddListener(() => SignUpButtonPressed?.Invoke());
            _skipButton.onClick.AddListener(() => SkipButtonPressed?.Invoke());
        }

        public override void SetInteractablesEnabled(bool value)
        {
            _loginButton.enabled = value;
            _signUpButton.enabled = value;
            _skipButton.enabled = value;
        }
    }
}