using Internal.Scripts._Bootstrap.Locator;
using Internal.Scripts.Core.Data.Items;
using Internal.Scripts.Features.Currency;

using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Scripts.Features.UI.Views.Shop
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Button _buyButton;

        private ItemSO _item;
        private Action<ItemSO> _onBuy;
        private ICurrencyService _currencyService;

        private readonly Color32 _cantBuyColor = new Color32(150, 150, 150, 150);

        public void Initialize(ItemSO item, Action<ItemSO> onBuy)
        {
            _item = item;
            _onBuy = onBuy;
            _currencyService = ServiceLocator.Get<ICurrencyService>();

            _icon.sprite = item.Icon;
            _nameText.text = item.Name;
            _descriptionText.text = item.Description;
            _priceText.text = $"{item.Price}$";

            _buyButton.onClick.AddListener(OnBuyClicked);
            _currencyService.Currency.OnValueChanged += OnCurrencyChanged;

            UpdateButtonState(_currencyService.Currency.Value);
        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveListener(OnBuyClicked);
            _currencyService.Currency.OnValueChanged -= OnCurrencyChanged;
        }

        private void OnBuyClicked() => _onBuy?.Invoke(_item);

        private void OnCurrencyChanged(int _, int newValue) => UpdateButtonState(newValue);

        private void UpdateButtonState(int currency)
        {
            var canBuy = currency >= _item.Price;
            
            _buyButton.interactable = canBuy;
            _icon.color = canBuy ? Color.white : _cantBuyColor;
        }
    }
}