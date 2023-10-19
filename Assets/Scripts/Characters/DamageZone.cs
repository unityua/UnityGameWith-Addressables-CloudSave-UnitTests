using UnityEngine;

namespace PesPatron.Characters
{
    public class DamageZone : MonoBehaviour
    {
        [SerializeField] private CharactersTrigger _damageZone;
        [SerializeField] private int _damage;

        private void Start()
        {
            _damageZone.CharacterEntered += OnCharacterEntered;
        }

        private void OnCharacterEntered(Character enteredCharacter)
        {
            if(enteredCharacter.TryGetComponent(out Health health))
            {
                health.ApplyDamage(_damage);
            }
        }
    }
}