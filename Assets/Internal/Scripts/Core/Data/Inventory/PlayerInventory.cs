using System;
using System.Linq;
using Internal.Scripts.Core.Data.Items;
using Internal.Scripts.Core.Reactive;
using Internal.Scripts.Core.Reactive.Readonly;
using UnityEngine;

namespace Internal.Scripts.Core.Data.Inventory
{
    [Serializable]
    public class PlayerInventory : IInventory
    {
        private const int DEFAULT_CAPACITY = 16;

        [SerializeField] private int _capacity;
        private readonly ReactiveList<InventoryItem> _items;

        public int Capacity => _capacity;
        public int UsedSlots => _items.GetRawList().Count(x => x != null);
        public bool IsFull => UsedSlots >= Capacity;
        public ReadOnlyReactiveList<InventoryItem> Items => _items.AsReadOnly();

        public PlayerInventory(int capacity = DEFAULT_CAPACITY)
        {
            _capacity = capacity;
            _items = new ReactiveList<InventoryItem>();

            for (int i = 0; i < capacity; i++)
                _items.Add(null, setSilent: true);
        }

        public bool TryAdd(ItemSO item, int amount = 1)
        {
            if (TryGetExisting(item, out var existing, out _))
            {
                existing.AddAmount(amount);
                var index = _items.IndexOf(existing);
                _items.SetValue(index, existing);
                
                return true;
            }

            if (!TryGetFreeSlot(out var freeSlot))
                return false;

            _items.SetValue(freeSlot, new InventoryItem(item, amount));
            return true;
        }

        public bool CanAdd(ItemSO item, int amount = 1)
        {
            if (TryGetExisting(item, out _, out _)) return true;
            if (TryGetFreeSlot(out _)) return true;
            return false;
        }

        public bool TryRemove(ItemSO item, int amount = 1)
        {
            if (!TryGetExisting(item, out var existing, out var index))
                return false;

            if (!existing.HasEnough(amount))
                return false;

            existing.RemoveAmount(amount);

            if (existing.Amount <= 0)
                _items.SetValue(index, null);
            else
                _items.SetValue(index, existing);

            return true;
        }

        public bool Contains(ItemSO item, int amount = 1)
        {
            return TryGetExisting(item, out var existing, out _) && existing.HasEnough(amount);
        }

        private bool TryGetFreeSlot(out int freeSlot)
        {
            freeSlot = -1;
            for (int i = 0; i < _capacity; i++)
            {
                if (_items[i] == null)
                {
                    freeSlot = i;
                    return true;
                }
            }

            return false;
        }

        private bool TryGetExisting(ItemSO item, out InventoryItem inventoryItem, out int index)
        {
            inventoryItem = null;
            index = -1;
            for (int i = 0; i < _capacity; i++)
            {
                if (_items[i]?.Item == item)
                {
                    inventoryItem = _items[i];
                    index = i;
                    return true;
                }
            }

            return false;
        }
    }
}