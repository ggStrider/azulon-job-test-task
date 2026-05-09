using Internal.Scripts.Core.Data.Inventory;
using Internal.Scripts.Core.Reactive;
using Internal.Scripts.Core.Reactive.Readonly;
using Internal.Scripts.Features.Currency;

namespace Internal.Scripts.Features.Click
{
    public class ClickService : IClickService
    {
        private const int BASE_CLICK_VALUE = 1;

        private readonly ICurrencyService _currencyService;
        private readonly IInventory _inventory;

        private readonly ReactiveVariable<int> _clickValue = new(BASE_CLICK_VALUE);
        public ReadOnlyReactiveVariable<int> ClickValue => _clickValue.AsReadOnly();

        public ClickService(ICurrencyService currencyService, IInventory inventory)
        {
            _currencyService = currencyService;
            _inventory = inventory;

            RecalculateClickValue();
        }

        public void Click()
        {
            _currencyService.Add(_clickValue.Value);
        }

        public void RecalculateClickValue()
        {
            var total = BASE_CLICK_VALUE;

            foreach (var slot in _inventory.Items.AsReadOnly())
            {
                if (slot == null) continue;
                total += slot.Item.ClickBonus * slot.Amount;
            }

            _clickValue.Value = total;
        }
    }
}