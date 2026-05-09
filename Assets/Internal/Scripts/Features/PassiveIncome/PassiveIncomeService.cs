using System;
using Internal.Scripts._Bootstrap.Tick;
using Internal.Scripts.Core.Data.Inventory;
using Internal.Scripts.Features.Currency;
using UnityEngine;

namespace Internal.Scripts.Features.PassiveIncome
{
    public class PassiveIncomeService : IPassiveIncomeService, ITickable
    {
        private readonly ICurrencyService _currencyService;
        private readonly IInventory _inventory;

        private float _accumulated;

        public float IncomePerSecond { get; private set; }

        public PassiveIncomeService(ICurrencyService currencyService, IInventory inventory)
        {
            _currencyService = currencyService;
            _inventory = inventory;
            
            _inventory.Items.OnListChanged += Recalculate;
            
            Recalculate();
        }
        
        public void Dispose()
        {
            if (_inventory != null)
            {
                _inventory.Items.OnListChanged -= Recalculate;
            }
        }

        public void Recalculate()
        {
            IncomePerSecond = 0f;

            foreach (var slot in _inventory.Items.AsReadOnly())
            {
                if (slot == null) continue;
                IncomePerSecond += slot.Item.PassiveIncomePerSecond * slot.Amount;
            }
        }

        public void Tick(float deltaTime)
        {
            if (IncomePerSecond <= 0f) return;

            _accumulated += IncomePerSecond * deltaTime;

            if (_accumulated >= 1f)
            {
                var toAdd = Mathf.FloorToInt(_accumulated);
                _accumulated -= toAdd;
                _currencyService.Add(toAdd);
            }
        }
    }
}