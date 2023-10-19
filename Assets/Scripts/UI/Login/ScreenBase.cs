using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PesPatron.UI
{
    public abstract class ScreenBase : MonoBehaviour
    {
        public abstract void Initialize();

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public abstract void SetInteractablesEnabled(bool value);
    }
}