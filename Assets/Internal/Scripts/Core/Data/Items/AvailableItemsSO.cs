using System.Collections.Generic;
using UnityEngine;

namespace Internal.Scripts.Core.Data.Items
{
    [CreateAssetMenu(fileName = "New Available Items List",
        menuName = "Game/Available Items List")]
    public class AvailableItemsSO : ScriptableObject
    {
        [SerializeField] private List<ItemSO> _items = new();

        public IReadOnlyList<ItemSO> Items => _items.AsReadOnly();
    }
}