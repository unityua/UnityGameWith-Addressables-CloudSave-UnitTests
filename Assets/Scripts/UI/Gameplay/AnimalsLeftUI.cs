using TMPro;
using UnityEngine;

namespace PesPatron.UI
{
    public class AnimalsLeftUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _animalsLeftLabel;

        private int _capturedAnimalsCount;
        private int _targetAnimalsCount;

        public void SetTargetAnimalsCount(int animalsCount)
        {
            _targetAnimalsCount = animalsCount;
            UpdateLabel();
        }

        public void SetCapturedAnimalsCount(int capturedAnimalsCount) 
        {
            _capturedAnimalsCount = capturedAnimalsCount;
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            _animalsLeftLabel.text = $"{_capturedAnimalsCount} / {_targetAnimalsCount}";
        }
    }
}