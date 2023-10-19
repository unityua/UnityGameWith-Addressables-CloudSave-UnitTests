using PesPatron.Characters;
using PesPatron.Pickables;
using UnityEngine;

namespace PesPatron.PlayerStuff
{
    public class Player : Character, IItemsPickerProvider
    {
        [Header("Player Data")]
        [SerializeField] private ItemsPicker _itemsPicker;
        [SerializeField] private Health _health;

        public ItemsPicker ItemPicker => _itemsPicker;
        public Health Health => _health;

        public void Initialize()
        {
            _movement.Initialize();
        }
    }
}