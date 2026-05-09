using System.Collections.Generic;
using Internal.Scripts.Core.Data.Items;

namespace Internal.Scripts.Core.Data.Inventory
{
    public interface IInventory
    {
        public int Capacity { get; }
        public int UsedSlots { get; }
        public bool IsFull { get; }
    
        public IReadOnlyList<InventoryItem> Items { get; }
    
        public bool TryAdd(ItemSO item, int amount = 1);
        public bool CanAdd(ItemSO item, int amount = 1);
        public bool TryRemove(ItemSO item, int amount = 1);
        public bool Contains(ItemSO item, int amount = 1);
    }
}