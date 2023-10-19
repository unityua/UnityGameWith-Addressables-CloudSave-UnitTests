using PesPatron.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PesPatron.UI
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text _winLabel;
        [SerializeField] private TMP_Text _loseLabel;
        [SerializeField] private TimerUI _timerUI;
        [Space]
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;

        private ISceneChanger _sceneChanger;

        public void Construct(ISceneChanger sceneChanger)
        {
            _sceneChanger = sceneChanger;
        }

        public void Initialize()
        {
            _restartButton.onClick.AddListener(OnReloadPressed);
            _mainMenuButton.onClick.AddListener(OnMainMenuPressed);
        }

        public void ShowLoseScreen(float passedTime )
        {
            gameObject.SetActive(true);

            _winLabel.gameObject.SetActive(false);
            _loseLabel.gameObject.SetActive(true);

            _timerUI.SetTime(passedTime);
        }

        public void ShowWinScreen(float passedTime)
        {
            gameObject.SetActive(true);

            _winLabel.gameObject.SetActive(true);
            _loseLabel.gameObject.SetActive(false);

            _timerUI.SetTime(passedTime);
        }

        private void OnReloadPressed()
        {
            _sceneChanger.Reload();

            DisableButtons();
        }

        private void OnMainMenuPressed()
        {
            _sceneChanger.LoadMainMenu();

            DisableButtons();
        }

        private void DisableButtons()
        {
            _restartButton.enabled = false;
            _mainMenuButton.enabled = false;
        }
    }
}