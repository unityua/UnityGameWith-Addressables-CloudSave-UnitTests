using PesPatron.Characters;
using PesPatron.Helpers;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PesPatron.Pickables
{
    public class ItemsUnloader : MonoBehaviour
    {
        [SerializeField] private CharactersTrigger _charactersTrigger;
        [Space]
        [SerializeField] private float _maxSpawnDistanceFromCenter = 5f;

        public event Action<ItemsUnloader, int> UnloadedItems;

        private void Start()
        {
            _charactersTrigger.CharacterEntered += OnCharacterEntered;
        }

        private void OnDestroy()
        {
            UnloadedItems = null;
        }

        public Vector3 GetRandomPositionInUnloadZone()
        {
            return transform.position + VectorExtentions.RandomDirectionXZ() * Random.Range(0f, _maxSpawnDistanceFromCenter);
        }

        private void OnCharacterEntered(Character character)
        {
            if(character.gameObject.TryGetComponent(out IItemsPickerProvider itemsPickerProvider))
            {
                int unloadItemsCount = itemsPickerProvider.ItemPicker.PickedpItemsCount;

                itemsPickerProvider.ItemPicker.UnloadAllItemsTo(this);

                UnloadedItems?.Invoke(this, unloadItemsCount);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, _maxSpawnDistanceFromCenter);
        }
#endif
    }
}