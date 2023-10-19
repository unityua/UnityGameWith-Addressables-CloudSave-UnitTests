using PesPatron.Core;
using TMPro;
using UnityEngine;

namespace PesPatron.UI
{
    public class PlayerIdLabelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [Space]
        [SerializeField] private string _noneIdMessage = "no auth";

        private void Start()
        {
            GlobalGameData globalGameData = ProjectServices.GetService<GlobalGameData>();

            if (globalGameData.WebServicesInitialized)
                _label.text += globalGameData.PlayerId;
            else
                _label.text += _noneIdMessage;
        }
    }
}