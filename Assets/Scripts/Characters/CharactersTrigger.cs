using System;
using UnityEngine;

namespace PesPatron.Characters
{
    public class CharactersTrigger : MonoBehaviour
    {
        public event Action<Character> CharacterEntered;
        public event Action<Character> CharacterExited;

        private Action<Character> _enteredCharacterEnabledAction;
        private Action<Character> _enteredCharacterDisabledAction;

        private void Awake()
        {
            _enteredCharacterDisabledAction = OnEnteredCharacterDisabled;
            _enteredCharacterEnabledAction = OnEnteredCharacterEnabled;
        }

        private void OnDestroy()
        {
            CharacterEntered = null;
            CharacterExited = null;
        }

        private void OnTriggerEnter(Collider enteredCollider)
        {
            if(enteredCollider.TryGetComponent(out Character enteredCharacter))
            {
                enteredCharacter.BecameDisabled.Add(_enteredCharacterDisabledAction);
                enteredCharacter.BecameEnabled.Add(_enteredCharacterEnabledAction);

                CharacterEntered?.Invoke(enteredCharacter);
            }
        }

        private void OnTriggerExit(Collider exitedCollider)
        {
            if (exitedCollider.TryGetComponent(out Character exitedCharacter))
            {
                exitedCharacter.BecameDisabled.Remove(_enteredCharacterDisabledAction);
                exitedCharacter.BecameEnabled.Remove(_enteredCharacterEnabledAction);

                CharacterExited?.Invoke(exitedCharacter);
            }
        }

        private void OnEnteredCharacterDisabled(Character character)
        {
            CharacterExited?.Invoke(character);
        }

        private void OnEnteredCharacterEnabled(Character character)
        {
            CharacterEntered.Invoke(character);
        }
    }
}