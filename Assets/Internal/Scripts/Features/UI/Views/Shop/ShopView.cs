using Internal.Scripts._Bootstrap.Locator;
using Internal.Scripts.Core.Data.Items;
using Internal.Scripts.Features.Shop;
using UnityEngine;

namespace Internal.Scripts.Features.UI.Views.Shop
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private ShopItemView _itemPrefab;

        private AvailableItemsSO _availableItems;
        private IShopService _shopService;

        private void Start()
        {
            _availableItems = ServiceLocator.Get<AvailableItemsSO>();
            _shopService = ServiceLocator.Get<IShopService>();
            
            SpawnItems();
        }

        private void SpawnItems()
        {
            for (var i = 0; i < _availableItems.Items.Count; i++)
            {
                var item = _availableItems.Items[i];
                
                var view = Instantiate(_itemPrefab, _content);
                view.Initialize(item, OnBuy);
            }
        }

        private void OnBuy(ItemSO item) => _shopService.TryBuy(item);
    }
}