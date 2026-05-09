using Internal.Scripts._Bootstrap.Locator;
using Internal.Scripts.Features.Click;
using Internal.Scripts.Features.Currency;
using TMPro;
using UnityEngine;

namespace Internal.Scripts.Features.UI.Views
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currencyText;
        [SerializeField] private TextMeshProUGUI _clickValueText;

        private ICurrencyService _currencyService;
        private IClickService _clickService;

        private void Start()
        {
            _currencyService = ServiceLocator.Get<ICurrencyService>();
            _clickService = ServiceLocator.Get<IClickService>();

            _currencyService.Currency.OnValueChanged += OnCurrencyChanged;
            _clickService.ClickValue.OnValueChanged += OnClickValueChanged;

            UpdateCurrencyText(_currencyService.Currency.Value);
            UpdateClickValueText(_clickService.ClickValue.Value);
        }

        private void OnDestroy()
        {
            if (_currencyService != null)
            {
                _currencyService.Currency.OnValueChanged -= OnCurrencyChanged;
            }

            if (_clickService != null)
            {
                _clickService.ClickValue.OnValueChanged -= OnClickValueChanged;
            }
        }

        private void OnCurrencyChanged(int _, int newValue)
        {
            UpdateCurrencyText(newValue);
        }

        private void OnClickValueChanged(int _, int newValue)
        {
            UpdateClickValueText(newValue);
        }

        private void UpdateCurrencyText(int value) => _currencyText.text = $"Currency: {value}";
        private void UpdateClickValueText(int value) => _clickValueText.text = $"+{value} per click";
    }
}