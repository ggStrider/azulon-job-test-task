using Internal.Scripts._Bootstrap.Locator;
using Internal.Scripts.Core.Data.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Internal.Scripts.Features.UI.Views.Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private InventorySlotView _slotPrefab;

        private IInventory _inventory;
        private readonly List<InventorySlotView> _slots = new();
        
        private void Start()
        {
            _inventory = ServiceLocator.Get<IInventory>();
            _inventory.Items.OnListChanged += Render;

            SpawnSlots();
            Render();
        }

        private void OnDestroy()
        {
            if (_inventory != null)
            {
                _inventory.Items.OnListChanged -= Render;
            }
        }

        private void SpawnSlots()
        {
            for (int i = 0; i < _inventory.Capacity; i++)
            {
                var slot = Instantiate(_slotPrefab, _content);
                _slots.Add(slot);
            }
        }

        private void Render()
        {
            for (int i = 0; i < _slots.Count; i++)
                _slots[i].Render(_inventory.Items[i]);
        }
    }
}