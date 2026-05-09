using Internal.Scripts._Bootstrap.Locator;
using Internal.Scripts._Bootstrap.Tick;
using Internal.Scripts.Core.Data.Inventory;
using Internal.Scripts.Core.Data.Items;
using Internal.Scripts.Features.Click;
using Internal.Scripts.Features.Currency;
using Internal.Scripts.Features.PassiveIncome;
using Internal.Scripts.Features.Shop;
using UnityEngine;

namespace Internal.Scripts._Bootstrap
{
    [DefaultExecutionOrder(-1000)]
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private AvailableItemsSO _availableItems;
        [SerializeField] private TickableRunner _tickableRunner;
        
        private void Awake()
        {
            var currencyService = new CurrencyService();
            var inventory = new PlayerInventory();
            var clickService = new ClickService(currencyService, inventory);
            var shopService = new ShopService(currencyService, inventory, clickService);
            var passiveIncome = new PassiveIncomeService(currencyService, inventory);

            ServiceLocator.Register<ICurrencyService>(to: currencyService);
            ServiceLocator.Register<IInventory>(to: inventory);
            ServiceLocator.Register<IClickService>(to: clickService);
            ServiceLocator.Register<IShopService>(to: shopService);
            
            ServiceLocator.Register<IPassiveIncomeService>(to: passiveIncome);
            _tickableRunner.Register(passiveIncome);

            ServiceLocator.Register<AvailableItemsSO>(to: _availableItems);
        }
    }
}