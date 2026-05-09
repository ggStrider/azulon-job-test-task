using Internal.Scripts._Bootstrap.Locator;

using Internal.Scripts.Core.Data.Inventory;
using Internal.Scripts.Features.Click;
using Internal.Scripts.Features.Currency;
using UnityEngine;

namespace Internal.Scripts._Bootstrap
{
    [DefaultExecutionOrder(-1000)]
    public class GameBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            var currencyService = new CurrencyService();
            var inventory = new PlayerInventory();
            var clickService = new ClickService(currencyService, inventory);

            ServiceLocator.Register<ICurrencyService>(currencyService);
            ServiceLocator.Register<IInventory>(inventory);
            ServiceLocator.Register<IClickService>(clickService);
        }
    }
}