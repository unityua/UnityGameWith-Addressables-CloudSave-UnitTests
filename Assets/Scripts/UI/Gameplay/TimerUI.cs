using PesPatron.Helpers;
using System;
using TMPro;
using UnityEngine;

namespace PesPatron.UI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerLabel;
        [SerializeField] private string _prefix = "Time: ";

        public void SetTime(float passedSeconds)
        {
            _timerLabel.text = _prefix + TimeUtils.TimeFormatToMMSS(passedSeconds);
        }
    }
}