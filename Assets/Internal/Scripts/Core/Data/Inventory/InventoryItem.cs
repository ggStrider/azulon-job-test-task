using System;
using Internal.Scripts.Core.Data.Items;

namespace Internal.Scripts.Core.Data.Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public ItemSO Item { get; }
        public int Amount { get; private set; }

        public InventoryItem(ItemSO item, int amount = 1)
        {
            Item = item;
            Amount = amount;
        }

        public void AddAmount(int amount) => Amount += amount;
        public void RemoveAmount(int amount) => Amount -= amount;
        public bool HasEnough(int amount) => Amount >= amount;
    }
}