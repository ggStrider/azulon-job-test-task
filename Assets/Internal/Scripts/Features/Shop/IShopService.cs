using Internal.Scripts.Core.Data.Items;

namespace Internal.Scripts.Features.Shop
{
    public interface IShopService
    {
        public bool TryBuy(ItemSO item);
    }
}