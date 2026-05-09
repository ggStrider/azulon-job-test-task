using Internal.Scripts.Core.Reactive;
using Internal.Scripts.Core.Reactive.Readonly;
using UnityEngine;

namespace Internal.Scripts.Features.Currency
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ReactiveVariable<int> _currency = new(0);
        public ReadOnlyReactiveVariable<int> Currency => _currency.AsReadOnly();
        
        public void Add(int amount)
        {
            if (amount <= 0)
            {
                Debug.Log($"[{nameof(CurrencyService)}] Add amount cannot be <= 0");
                return;
            }
            
            _currency.Value += amount;
        }

        public bool TrySpend(int amount)
        {
            if (amount <= 0)
            {
                Debug.Log($"[{nameof(CurrencyService)}] Spend amount cannot be <= 0");
                return false;
            }

            if (_currency.Value < amount)
                return false;
            
            _currency.Value -= amount;
            return true;
        }
    }
}