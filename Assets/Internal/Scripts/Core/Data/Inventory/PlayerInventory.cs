using System;
using System.Collections.Generic;
using System.Linq;
using Internal.Scripts.Core.Data.Items;
using UnityEngine;

namespace Internal.Scripts.Core.Data.Inventory
{
    [Serializable]
    public class PlayerInventory : IInventory
    {
        private const int DEFAULT_CAPACITY = 16;

        [SerializeField] private List<InventoryItem> _items;
        [SerializeField] private int _capacity;

        public int Capacity => _capacity;
        public int UsedSlots => _items.Count(x => x != null);
        public bool IsFull => UsedSlots >= Capacity;

        public IReadOnlyList<InventoryItem> Items => _items.AsReadOnly();

        public PlayerInventory(int capacity = DEFAULT_CAPACITY)
        {
            _capacity = capacity;
            _items = new List<InventoryItem>(capacity);

            for (int i = 0; i < capacity; i++)
                _items.Add(null);
        }

        public bool TryAdd(ItemSO item, int amount = 1)
        {
            if (TryGetExisting(item, out var existing))
            {
                existing.AddAmount(amount);
                return true;
            }

            if (!TryGetFreeSlot(out var freeSlot))
                return false;

            _items[freeSlot] = new InventoryItem(item, amount);
            return true;
        }

        public bool TryRemove(ItemSO item, int amount = 1)
        {
            if (!TryGetExisting(item, out var existing))
                return false;

            if (!existing.HasEnough(amount))
                return false;

            existing.RemoveAmount(amount);

            if (existing.Amount <= 0)
                ClearSlot(_items.IndexOf(existing));

            return true;
        }

        public bool Contains(ItemSO item, int amount = 1)
        {
            return TryGetExisting(item, out var existing) && existing.HasEnough(amount);
        }

        private void ClearSlot(int index) => 
            _items[index] = null;

        private bool TryGetFreeSlot(out int freeSlot)
        {
            freeSlot = -1;
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] == null)
                {
                    freeSlot = i;
                    return true;
                }
            }

            return false;
        }

        private bool TryGetExisting(ItemSO item, out InventoryItem inventoryItem)
        {
            inventoryItem = null;
            foreach (var slot in _items)
            {
                if (slot?.Item == item)
                {
                    inventoryItem = slot;
                    return true;
                }
            }

            return false;
        }
    }
}