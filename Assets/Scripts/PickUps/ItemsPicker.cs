using PesPatron.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace PesPatron.Pickables
{
    public class ItemsPicker : MonoBehaviour, IItemsPickerProvider
    {
        [SerializeField] private CharactersTrigger _charactersTrigger;

        private Vector3 _nextItemPosition;
        private List<Character> _pickedUpItems = new List<Character>();

        private List<Character> _previousPickedItems = new List<Character>();

        public int PickedpItemsCount => _pickedUpItems.Count;   
        public ItemsPicker ItemPicker => this;

        private void Start()
        {
            _charactersTrigger.CharacterEntered += OnCharacterEntered;
        }

        public void UnloadAllItemsTo(ItemsUnloader itemsUnloader)
        {
            for (int i = _pickedUpItems.Count - 1; i >= 0; i--)
            {
                UnloadCharacter(_pickedUpItems[i], itemsUnloader.GetRandomPositionInUnloadZone());
            }

            _nextItemPosition = Vector3.zero;
            _pickedUpItems.Clear();
        }

        private void OnCharacterEntered(Character character)
        {
            if(character.TryGetComponent(out Pickable pickable) && _previousPickedItems.Contains(character) == false)
            {
                PickUpCharacter(character, pickable);
            }
        }

        private void UnloadCharacter(Character character, Vector3 position)
        {
            character.transform.SetParent(null);
            character.transform.SetPositionAndRotation(position, Quaternion.identity);

            character.EnableCharacter();
        }

        private void PickUpCharacter(Character character, Pickable pickable)
        {
            _pickedUpItems.Add(character);
            _previousPickedItems.Add(character);

            character.DisableCharacter();

            Vector3 characterLocalPosition = _nextItemPosition;

            if (pickable.FlipOnPickup)
                characterLocalPosition.y += pickable.Height;

            character.transform.SetParent(transform);
            character.transform.SetLocalPositionAndRotation(characterLocalPosition, Quaternion.Euler(pickable.PickedUpLocalRotation));

            _nextItemPosition.y += pickable.Height;
        }
    }
}