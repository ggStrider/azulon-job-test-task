using Internal.Scripts.Core.Data.Inventory;
using Internal.Scripts.Core.Data.Items;
using Internal.Scripts.Features.Currency;

namespace Internal.Scripts.Features.Shop
{
    public class ShopService : IShopService
    {
        private readonly ICurrencyService _currencyService;
        private readonly IInventory _inventory;

        public ShopService(ICurrencyService currencyService, IInventory inventory)
        {
            _currencyService = currencyService;
            _inventory = inventory;
        }

        public bool TryBuy(ItemSO item)
        {
            if (!_inventory.CanAdd(item))
                return false;

            if (!_currencyService.TrySpend(item.Price))
                return false;

            _inventory.TryAdd(item);
            return true;
        }
    }
}