using Internal.Scripts._Bootstrap.Locator;
using Internal.Scripts.Core.Data.Inventory;
using Internal.Scripts.Core.Data.Items;
using Internal.Scripts.Features.Click;
using Internal.Scripts.Features.Currency;
using Internal.Scripts.Features.Shop;
using UnityEngine;

namespace Internal.Scripts._Bootstrap
{
    [DefaultExecutionOrder(-1000)]
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private AvailableItemsSO _availableItems;
        
        private void Awake()
        {
            var currencyService = new CurrencyService();
            var inventory = new PlayerInventory();
            var clickService = new ClickService(currencyService, inventory);
            var shopService = new ShopService(currencyService, inventory, clickService);

            ServiceLocator.Register<ICurrencyService>(to: currencyService);
            ServiceLocator.Register<IInventory>(to: inventory);
            ServiceLocator.Register<IClickService>(to: clickService);
            ServiceLocator.Register<IShopService>(to: shopService);

            ServiceLocator.Register<AvailableItemsSO>(to: _availableItems);
        }
    }
}