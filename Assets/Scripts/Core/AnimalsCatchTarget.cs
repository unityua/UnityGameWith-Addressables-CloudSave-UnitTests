using PesPatron.Characters;
using PesPatron.Pickables;
using System;
using UnityEngine;

namespace PesPatron.Core
{
    public class AnimalsCatchTarget : MonoBehaviour
    {
        [SerializeField] private Character[] _charactersToCatch;
        [SerializeField] private ItemsUnloader[] _itemUnloaders;

        private int _unloadedCount;

        public int TotalTargetsToCatch => _charactersToCatch.Length;

        public event Action<AnimalsCatchTarget, int> CatchedTargetsCountChanged;
        public event Action<AnimalsCatchTarget> AllTargetsCatched;

        private void OnDestroy()
        {
            CatchedTargetsCountChanged = null;
            AllTargetsCatched = null;
        }

        public void Initialize()
        {
            foreach (var unloader in _itemUnloaders)
            {
                unloader.UnloadedItems += OnUnloadedItems;
            }
        }

        private void OnUnloadedItems(ItemsUnloader sender, int unloadedCount)
        {
            _unloadedCount += unloadedCount;
            CatchedTargetsCountChanged?.Invoke(this, _unloadedCount);

            if (_unloadedCount >= TotalTargetsToCatch)
                AllTargetsCatched?.Invoke(this);
        }
    }
}