using PesPatron.Helpers;
using UnityEngine;

namespace PesPatron.Characters
{
    public class Character : MonoBehaviour
    {
        [Header("Character Data")]
        [SerializeField] private CharacterType _characterType;
        [Space]
        [SerializeField] protected CharacterMovement _movement;
        [SerializeField] protected CharacterAnimator _movementAnimation;
        [Space]
        [SerializeField] protected Transform _visualParent;
        [SerializeField] protected CharacterVisual _currentVisual;

        public NonAllocEvent<Character> BecameEnabled { get; private set; } = new();
        public NonAllocEvent<Character> BecameDisabled { get; private set; } = new();

        private void OnDestroy()
        {
            BecameEnabled.ClearAllActions();
            BecameDisabled.ClearAllActions();
        }

        public void EnableCharacter()
        {
            _movement.enabled = true;
            _movementAnimation.enabled = true;
            _movement.SetCharacterControllerEnabled(true);

            BecameEnabled.Invoke(this);
        }

        public void DisableCharacter()
        {
            _movement.enabled = false;
            _movementAnimation.enabled = false;
            _movementAnimation.ResetAllAnimations();
            _movement.SetCharacterControllerEnabled(false);

            BecameDisabled.Invoke(this);
        }

        public void SetVisual(CharacterVisual newVisual)
        {
            if (_currentVisual != null)
            {
                Destroy(_currentVisual.gameObject);
            }

            newVisual.transform.SetParent(_visualParent);
            newVisual.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            _movementAnimation.SetAnimator(newVisual.Animator);
            _currentVisual = newVisual;
        }

        public CharacterMovement Movement => _movement;
        public CharacterType CharacterType => _characterType;
        public Vector3 Position => transform.position;
    }
}